using System;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

namespace ByteBank1
{

    public class Program
    {

        static void ShowMenu()
        {
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.Write("Digite a opção desejada: ");
        }

        static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Digite a senha: ");
            senhas.Add(Console.ReadLine());
            saldos.Add(0);
        }

        static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1)
            {
                Console.WriteLine("Não foi possível deletar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            cpfs.Remove(cpfParaDeletar);
            titulares.RemoveAt(indexParaDeletar);
            senhas.RemoveAt(indexParaDeletar);
            saldos.RemoveAt(indexParaDeletar);

            Console.WriteLine("Conta deletada com sucesso");
        }

        static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++)
            {
                ApresentaConta(i, cpfs, titulares, saldos);
            }
        }

        static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaApresentar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaApresentar == -1)
            {
                Console.WriteLine("Não foi possível apresentar esta Conta");
                Console.WriteLine("MOTIVO: Conta não encontrada.");
            }

            ApresentaConta(indexParaApresentar, cpfs, titulares, saldos);
        }

        static void ApresentarValorAcumulado(List<double> saldos)
        {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
        }

        static void ApresentaConta(int index, List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R${saldos[index]:F2}");
        }

        static void ManipularConta(List<string> cpfs,List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf:");
            string cpfUsuarioVerificar = Console.ReadLine();
            Console.Write("Digite a senha:");
            string senhaUsuarioVerificar = Console.ReadLine();
            Console.WriteLine("----------------");
            bool checarUsuario = false;
            bool checarSenha = false;
            int indexDaConta = 0;
            for(int i = 0; i< cpfs.Count; i++)
            {
                if (cpfs[i] == cpfUsuarioVerificar && senhaUsuarioVerificar == senhas[i] )
                {
                    checarUsuario = true;
                    checarSenha = true;
                    indexDaConta = i;
                }
            }

            int escolha;
            if (checarUsuario && checarSenha)
            {
                do
                {
                    Console.WriteLine("Conta acessada.");
                    Console.WriteLine($"CPF = {cpfs[indexDaConta]} | Titular = {titulares[indexDaConta]} | Saldo = R${saldos[indexDaConta]:F2}");
                    ShowMenuConta();
                    escolha = int.Parse(Console.ReadLine());
                    switch (escolha)
                    {
                        case 1:
                            DepositarNaConta(indexDaConta,saldos);
                            break;
                        case 2:
                            SacaDaConta(indexDaConta,saldos);
                            break;
                        case 3:
                            transferirDaConta(indexDaConta, cpfs, saldos);
                            break;

                    }
                } while (escolha != 0);
            }
            else
            {
                Console.WriteLine("Usuario invalido voltando ao menu principal");
            }
        }

        static void ShowMenuConta()
        {
            Console.WriteLine("1 - Depositar");
            Console.WriteLine("2 - Saque");
            Console.WriteLine("3 - Transferencia");
            Console.WriteLine("0 - Voltar");
            Console.Write("Digite a opção desejada: ");
        }
        static void DepositarNaConta(int index,List<double> saldos)
        {
            Console.WriteLine($"-----------------------------------");
            Console.Write("Digite a quantia desejada:");
            double Valor = double.Parse(Console.ReadLine());
            saldos[index] += Valor;
            Console.WriteLine($"-----------------------------------");
            Console.WriteLine($"Deposito realizado com sucesso");
        }

        static void SacaDaConta(int index, List<double> saldos)
        {
            Console.WriteLine($"-------------------------------");
            Console.Write("Digite a quantia desejada para saque:");
            double Valor = double.Parse(Console.ReadLine());
            if (saldos[index] > Valor)
            {
                saldos[index] -= Valor;
                Console.WriteLine($"---------------------------");
                Console.WriteLine($"Saque realizado com sucesso.");
            }
            else
            {
                Console.WriteLine("Quantia invalida, menor do que o seu total disponivel.");
            }
        }
        static void transferirDaConta(int index,List<string> cpfs, List<double> saldos)
        {
            Console.WriteLine($"-----------------------------------");
            Console.Write("Digite a quantia desejada para transferencia:");
            double Valor = double.Parse(Console.ReadLine());
            if (saldos[index] >= Valor) 
            {
                Console.Write("Qual o cpf da conta que deseja transferir:");
                string cpfConta = Console.ReadLine();
                int indexContaTransferencia = -1;
                for (int i = 0; i < cpfs.Count; i++)
                {
                    if (cpfs[i] == cpfConta)
                    {
                        indexContaTransferencia = i;
                    }
                }
                if (indexContaTransferencia >= 0 )
                {
                    saldos[indexContaTransferencia] += Valor;
                    saldos[index] -= Valor;
                    Console.WriteLine($"-----------------------------------");
                    Console.WriteLine($"transferencia realizada com sucesso");
                }
                else
                {
                    Console.WriteLine("Essa conta digitada não existe.");
                }
            }
            else
            {
                Console.WriteLine("Quantia desejada menor que o seu total disponivel.");
            }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
            int option;

            do
            {
                ShowMenu();
                option = int.Parse(Console.ReadLine());

                Console.WriteLine("-----------------");

                switch (option)
                {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 6:
                        ManipularConta(cpfs,titulares, senhas, saldos);
                        break;
                }

                Console.WriteLine("-----------------");
            } while (option != 0);

        }

    }

}
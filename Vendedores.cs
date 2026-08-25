using System;

namespace EstruturaDeDados2
{
    // ==================== CLASSE VENDA ====================
    public class Venda
    {
        private int qtde;
        private double valor;

        public int Qtde
        {
            get => qtde;
            set => qtde = value;
        }

        public double Valor
        {
            get => valor;
            set => valor = value;
        }

        public Venda() : this(0, 0.0) { }

        public Venda(int qtde, double valor)
        {
            this.qtde = qtde;
            this.valor = valor;
        }

        public double ValorMedio()
        {
            if (qtde == 0) return 0.0;
            return valor / qtde;
        }
    }

    // ==================== CLASSE VENDEDOR ====================
    public class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Nome
        {
            get => nome;
            set => nome = value;
        }

        public double PercComissao
        {
            get => percComissao;
            set => percComissao = value;
        }

        public Venda[] AsVendas => asVendas;

        public Vendedor() : this(0, "", 0.0) { }

        public Vendedor(int id, string nome, double percComissao)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;
            this.asVendas = new Venda[31];
            for (int i = 0; i < 31; i++)
            {
                this.asVendas[i] = new Venda();
            }
        }

        public void RegistrarVenda(int dia, Venda venda)
        {
            if (dia >= 1 && dia <= 31)
            {
                // Acumula quantidade e valor se já houver vendas no dia
                asVendas[dia - 1].Qtde += venda.Qtde;
                asVendas[dia - 1].Valor += venda.Valor;
            }
        }

        public double ValorVendas()
        {
            double total = 0.0;
            foreach (var v in asVendas)
            {
                total += v.Valor;
            }
            return total;
        }

        public double ValorComissao()
        {
            return ValorVendas() * (percComissao / 100.0);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vendedor v)
            {
                return this.id == v.id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }

    // ==================== CLASSE VENDEDORES ====================
    public class Vendedores
    {
        private Vendedor[] osVendedores;
        private int max;
        private int qtde;

        public Vendedor[] OsVendedores => osVendedores;
        public int Qtde => qtde;
        public int Max => max;

        public Vendedores(int max)
        {
            this.max = max;
            this.qtde = 0;
            this.osVendedores = new Vendedor[max];
        }

        public bool AddVendedor(Vendedor v)
        {
            if (qtde >= max) return false;
            if (SearchVendedor(v).Id != -1) return false; // Já existe ID igual

            osVendedores[qtde] = v;
            qtde++;
            return true;
        }

        public bool DelVendedor(Vendedor v)
        {
            int index = -1;
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Equals(v))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1) return false;

            // (*) Não permite excluir se houver vendas registradas
            if (osVendedores[index].ValorVendas() > 0) return false;

            // Desloca os elementos no array
            for (int i = index; i < qtde - 1; i++)
            {
                osVendedores[i] = osVendedores[i + 1];
            }
            osVendedores[qtde - 1] = null;
            qtde--;
            return true;
        }

        public Vendedor SearchVendedor(Vendedor v)
        {
            for (int i = 0; i < qtde; i++)
            {
                if (osVendedores[i].Equals(v))
                {
                    return osVendedores[i];
                }
            }
            return new Vendedor(-1, "", 0.0); // Retorna dummy com ID -1 se não achar
        }

        public double ValorVendas()
        {
            double total = 0.0;
            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].ValorVendas();
            }
            return total;
        }

        public double ValorComissao()
        {
            double total = 0.0;
            for (int i = 0; i < qtde; i++)
            {
                total += osVendedores[i].ValorComissao();
            }
            return total;
        }
    }

    // ==================== PROGRAMA PRINCIPAL ====================
    class Program
    {
        static void Main(string[] args)
        {
            Vendedores cadastro = new Vendedores(10);
            int opcao = -1;

            do
            {
                Console.WriteLine("\n================ SELETOR DE OPÇÕES ================");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Cadastrar vendedor");
                Console.WriteLine("2. Consultar vendedor");
                Console.WriteLine("3. Excluir vendedor");
                Console.WriteLine("4. Registrar venda");
                Console.WriteLine("5. Listar vendedores");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    Console.WriteLine("Opção inválida! Digite um número.");
                    continue;
                }

                switch (opcao)
                {
                    case 0:
                        Console.WriteLine("Encerrando o programa...");
                        break;

                    case 1: // Cadastrar vendedor
                        if (cadastro.Qtde >= cadastro.Max)
                        {
                            Console.WriteLine("Erro: Limite máximo de vendedores cadastrados atingido (máx 10).");
                            break;
                        }

                        Console.Write("Digite o ID do vendedor: ");
                        int idAdd = int.Parse(Console.ReadLine());

                        Vendedor vBusca = cadastro.SearchVendedor(new Vendedor(idAdd, "", 0));
                        if (vBusca.Id != -1)
                        {
                            Console.WriteLine("Erro: Já existe um vendedor cadastrado com esse ID.");
                            break;
                        }

                        Console.Write("Digite o nome do vendedor: ");
                        string nome = Console.ReadLine();
                        Console.Write("Digite a % de comissão (ex: 10 para 10%): ");
                        double comissao = double.Parse(Console.ReadLine());

                        Vendedor novo = new Vendedor(idAdd, nome, comissao);
                        if (cadastro.AddVendedor(novo))
                            Console.WriteLine("Vendedor cadastrado com sucesso!");
                        else
                            Console.WriteLine("Falha ao cadastrar vendedor.");
                        break;

                    case 2: // Consultar vendedor
                        Console.Write("Digite o ID do vendedor para consulta: ");
                        int idConsultar = int.Parse(Console.ReadLine());
                        Vendedor vEncontrado = cadastro.SearchVendedor(new Vendedor(idConsultar, "", 0));

                        if (vEncontrado.Id == -1)
                        {
                            Console.WriteLine("Vendedor não encontrado!");
                        }
                        else
                        {
                            Console.WriteLine($"\nID: {vEncontrado.Id} | Nome: {vEncontrado.Nome}");
                            Console.WriteLine($"Valor total das vendas: R$ {vEncontrado.ValorVendas():F2}");
                            Console.WriteLine($"Valor da comissão devida: R$ {vEncontrado.ValorComissao():F2}");
                            Console.WriteLine("--- Valor médio das vendas diárias ---");
                            
                            bool teveVenda = false;
                            for (int i = 0; i < 31; i++)
                            {
                                Venda vDia = vEncontrado.AsVendas[i];
                                if (vDia.Qtde > 0)
                                {
                                    teveVenda = true;
                                    Console.WriteLine($"Dia {i + 1:D2}: Valor Médio por Venda = R$ {vDia.ValorMedio():F2} ({vDia.Qtde} vendas, total R$ {vDia.Valor:F2})");
                                }
                            }
                            if (!teveVenda)
                            {
                                Console.WriteLine("Nenhuma venda registrada no mês.");
                            }
                        }
                        break;

                    case 3: // Excluir vendedor
                        Console.Write("Digite o ID do vendedor a ser excluído: ");
                        int idDel = int.Parse(Console.ReadLine());
                        Vendedor vDel = cadastro.SearchVendedor(new Vendedor(idDel, "", 0));

                        if (vDel.Id == -1)
                        {
                            Console.WriteLine("Vendedor não encontrado!");
                        }
                        else
                        {
                            if (cadastro.DelVendedor(vDel))
                            {
                                Console.WriteLine("Vendedor excluído com sucesso!");
                            }
                            else
                            {
                                Console.WriteLine("Erro: Não é possível excluir vendedor com vendas registradas!");
                            }
                        }
                        break;

                    case 4: // Registrar venda
                        Console.Write("Digite o ID do vendedor: ");
                        int idVenda = int.Parse(Console.ReadLine());
                        Vendedor vReg = cadastro.SearchVendedor(new Vendedor(idVenda, "", 0));

                        if (vReg.Id == -1)
                        {
                            Console.WriteLine("Vendedor não encontrado!");
                        }
                        else
                        {
                            Console.Write("Digite o dia do mês (1 a 31): ");
                            int dia = int.Parse(Console.ReadLine());

                            if (dia < 1 || dia > 31)
                            {
                                Console.WriteLine("Dia inválido!");
                                break;
                            }

                            Console.Write("Digite a quantidade de vendas: ");
                            int qtdeVendas = int.Parse(Console.ReadLine());
                            Console.Write("Digite o valor total dessas vendas: R$ ");
                            double valorVendas = double.Parse(Console.ReadLine());

                            vReg.RegistrarVenda(dia, new Venda(qtdeVendas, valorVendas));
                            Console.WriteLine("Venda registrada com sucesso!");
                        }
                        break;

                    case 5: // Listar vendedores
                        if (cadastro.Qtde == 0)
                        {
                            Console.WriteLine("Nenhum vendedor cadastrado.");
                        }
                        else
                        {
                            Console.WriteLine("\n========== LISTA DE VENDEDORES ==========");
                            for (int i = 0; i < cadastro.Qtde; i++)
                            {
                                Vendedor v = cadastro.OsVendedores[i];
                                Console.WriteLine($"ID: {v.Id,-5} | Nome: {v.Nome,-20} | Total Vendas: R$ {v.ValorVendas(),10:F2} | Comissão: R$ {v.ValorComissao(),10:F2}");
                            }
                            Console.WriteLine("-----------------------------------------");
                            Console.WriteLine($"TOTALIZADOR GERAL DE VENDAS  : R$ {cadastro.ValorVendas():F2}");
                            Console.WriteLine($"TOTALIZADOR GERAL DE COMISSÃO: R$ {cadastro.ValorComissao():F2}");
                        }
                        break;

                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

            } while (opcao != 0);
        }
    }
}

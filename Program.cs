using System;
using System.Collections.Generic;
using System.Linq;

class Tarefa
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int Prioridade { get; set; }
    public string Status { get; private set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataConclusao { get; private set; }

    public Tarefa(int id, string titulo, string descricao, int prioridade)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        Prioridade = prioridade;
        Status = "Aberta";
        DataCriacao = DateTime.Now;
        DataConclusao = null;
    }

    public void Concluir()
    {
        if (Status == "Aberta")
        {
            Status = "Fechada";
            DataConclusao = DateTime.Now;
        }
    }

    public void Cancelar()
    {
        if (Status == "Aberta")
        {
            Status = "Cancelada";
            DataConclusao = null;
        }
    }

    public void Reabrir()
    {
        if (Status == "Fechada" || Status == "Cancelada")
        {
            Status = "Aberta";
            DataConclusao = null;
        }
    }
}

class Projeto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();

    // Corrigido: adicionado o modificador 'public'
    public Projeto(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }

    public void AdicionarTarefa(Tarefa t)
    {
        Tarefas.Add(t);
    }

    public bool RemoverTarefa(Tarefa t)
    {
        return Tarefas.Remove(t);
    }

    public Tarefa BuscarTarefa(Tarefa t)
    {
        return Tarefas.FirstOrDefault(x => x.Id == t.Id);
    }

    public List<Tarefa> TarefasPorStatus(string s)
    {
        return Tarefas.Where(t => t.Status.Equals(s, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Tarefa> TarefasPorPrioridade(int p)
    {
        return Tarefas.Where(t => t.Prioridade == p).ToList();
    }

    public int TotalAbertas()
    {
        return Tarefas.Count(t => t.Status == "Aberta");
    }

    public int TotalFechadas()
    {
        return Tarefas.Count(t => t.Status == "Fechada");
    }
}

class Projetos
{
    private List<Projeto> itens = new List<Projeto>();

    public bool Adicionar(Projeto p)
    {
        if (itens.Any(x => x.Id == p.Id || x.Nome.Equals(p.Nome, StringComparison.OrdinalIgnoreCase)))
            return false;

        itens.Add(p);
        return true;
    }

    public bool Remover(Projeto p)
    {
        if (p == null || p.Tarefas.Count > 0)
            return false;

        return itens.Remove(p);
    }

    public Projeto Buscar(Projeto p)
    {
        return itens.FirstOrDefault(x => x.Id == p.Id);
    }

    public List<Projeto> Listar()
    {
        return itens;
    }
}

class Program
{
    static Projetos projetos = new Projetos();
    static int proximoIdProjeto = 1;
    static int proximoIdTarefa = 1;

    static void Main()
    {
        int opcao;

        do
        {
            Console.Clear();
            Console.WriteLine("===== GERENCIADOR DE PROJETOS =====");
            Console.WriteLine("0. Sair");
            Console.WriteLine("1. Adicionar projeto");
            Console.WriteLine("2. Pesquisar projeto (mostrar tarefas por status e totais)");
            Console.WriteLine("3. Remover projeto (apenas se sem tarefas)");
            Console.WriteLine("4. Adicionar tarefa em projeto");
            Console.WriteLine("5. Concluir tarefa");
            Console.WriteLine("6. Cancelar tarefa");
            Console.WriteLine("7. Reabrir tarefa");
            Console.WriteLine("8. Listar tarefas de um projeto");
            Console.WriteLine("9. Filtrar tarefas por status ou prioridade em um projeto");
            Console.WriteLine("10. Filtrar tarefas por status ou prioridade em todos os projetos");
            Console.WriteLine("11. Resumo geral");
            Console.Write("Escolha uma opção: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
                opcao = -1;

            Console.Clear();

            switch (opcao)
            {
                case 0:
                    Console.WriteLine("Encerrando o sistema...");
                    break;
                case 1:
                    AdicionarProjeto();
                    break;
                case 2:
                    PesquisarProjeto();
                    break;
                case 3:
                    RemoverProjeto();
                    break;
                case 4:
                    AdicionarTarefa();
                    break;
                case 5:
                    AlterarStatusTarefa(1);
                    break;
                case 6:
                    AlterarStatusTarefa(2);
                    break;
                case 7:
                    AlterarStatusTarefa(3);
                    break;
                case 8:
                    ListarTarefasProjeto();
                    break;
                case 9:
                    FiltrarTarefasProjeto();
                    break;
                case 10:
                    FiltrarTarefasTodosProjetos();
                    break;
                case 11:
                    ResumoGeral();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (opcao != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        while (opcao != 0);
    }

    static void AdicionarProjeto()
    {
        Console.Write("Nome do projeto: ");
        string nome = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("O nome do projeto não pode ser vazio.");
            return;
        }

        Projeto projeto = new Projeto(proximoIdProjeto, nome);

        if (projetos.Adicionar(projeto))
        {
            proximoIdProjeto++;
            Console.WriteLine($"Projeto cadastrado com sucesso. ID: {projeto.Id}");
        }
        else
        {
            Console.WriteLine("Já existe um projeto com esse ID ou nome.");
        }
    }

    static Projeto SelecionarProjeto()
    {
        List<Projeto> lista = projetos.Listar();

        if (lista.Count == 0)
        {
            Console.WriteLine("Nenhum projeto cadastrado.");
            return null;
        }

        Console.WriteLine("===== PROJETOS CADASTRADOS =====");

        foreach (Projeto p in lista)
            Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Tarefas: {p.Tarefas.Count}");

        Console.Write("Informe o ID do projeto: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return null;
        }

        Projeto projeto = lista.FirstOrDefault(p => p.Id == id);

        if (projeto == null)
            Console.WriteLine("Projeto não encontrado.");

        return projeto;
    }

    static void PesquisarProjeto()
    {
        Projeto projeto = SelecionarProjeto();

        if (projeto == null)
            return;

        Console.WriteLine($"\nProjeto: {projeto.Nome}");
        Console.WriteLine($"Total de tarefas: {projeto.Tarefas.Count}");
        Console.WriteLine($"Tarefas abertas: {projeto.TotalAbertas()}");
        Console.WriteLine($"Tarefas fechadas: {projeto.TotalFechadas()}");
        Console.WriteLine($"Tarefas canceladas: {projeto.Tarefas.Count(t => t.Status == "Cancelada")}");

        foreach (string status in new[] { "Aberta", "Fechada", "Cancelada" })
        {
            Console.WriteLine($"\n--- Tarefas {status} ---");
            ExibirTarefas(projeto.TarefasPorStatus(status));
        }
    }

    static void RemoverProjeto()
    {
        Projeto projeto = SelecionarProjeto();

        if (projeto == null)
            return;

        if (projetos.Remover(projeto))
            Console.WriteLine("Projeto removido com sucesso.");
        else
            Console.WriteLine("Não é possível remover um projeto que possui tarefas.");
    }

    static void AdicionarTarefa()
    {
        Projeto projeto = SelecionarProjeto();

        if (projeto == null)
            return;

        Console.Write("Título da tarefa: ");
        string titulo = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(titulo))
        {
            Console.WriteLine("O título não pode ser vazio.");
            return;
        }

        Console.Write("Descrição: ");
        string descricao = Console.ReadLine() ?? "";

        Console.WriteLine("Prioridade: 1 - Alta | 2 - Média | 3 - Baixa");
        Console.Write("Escolha a prioridade: ");

        if (!int.TryParse(Console.ReadLine(), out int prioridade) || prioridade < 1 || prioridade > 3)
        {
            Console.WriteLine("Prioridade inválida.");
            return;
        }

        Tarefa tarefa = new Tarefa(proximoIdTarefa, titulo, descricao, prioridade);
        projeto.AdicionarTarefa(tarefa);
        proximoIdTarefa++;

        Console.WriteLine($"Tarefa cadastrada com sucesso. ID: {tarefa.Id}");
    }

    static void AlterarStatusTarefa(int operacao)
    {
        Projeto projeto = SelecionarProjeto();

        if (projeto == null)
            return;

        if (projeto.Tarefas.Count == 0)
        {
            Console.WriteLine("Este projeto não possui tarefas.");
            return;
        }

        ExibirTarefas(projeto.Tarefas);

        Console.Write("Informe o ID da tarefa: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID inválido.");
            return;
        }

        Tarefa tarefa = projeto.Tarefas.FirstOrDefault(t => t.Id == id);

        if (tarefa == null)
        {
            Console.WriteLine("Tarefa não encontrada neste projeto.");
            return;
        }

        if (operacao == 1)
        {
            if (tarefa.Status != "Aberta")
            {
                Console.WriteLine("Somente tarefas abertas podem ser concluídas.");
                return;
            }

            tarefa.Concluir();
            Console.WriteLine("Tarefa concluída com sucesso.");
        }
        else if (operacao == 2)
        {
            if (tarefa.Status != "Aberta")
            {
                Console.WriteLine("Somente tarefas abertas podem ser canceladas.");
                return;
            }

            tarefa.Cancelar();
            Console.WriteLine("Tarefa cancelada com sucesso.");
        }
        else
        {
            if (tarefa.Status == "Aberta")
            {
                Console.WriteLine("A tarefa já está aberta.");
                return;
            }

            tarefa.Reabrir();
            Console.WriteLine("Tarefa reaberta com sucesso.");
        }
    }

    static void ListarTarefasProjeto()
    {
        Projeto projeto = SelecionarProjeto();

        if (projeto == null)
            return;

        Console.WriteLine($"\nTarefas do projeto: {projeto.Nome}");
        ExibirTarefas(projeto.Tarefas);
    }

    static void FiltrarTarefasProjeto()
    {
        Projeto projeto = SelecionarProjeto();

        if (projeto == null)
            return;

        Console.WriteLine("1. Filtrar por status");
        Console.WriteLine("2. Filtrar por prioridade");
        Console.Write("Escolha: ");

        if (!int.TryParse(Console.ReadLine(), out int opcao))
        {
            Console.WriteLine("Opção inválida.");
            return;
        }

        if (opcao == 1)
        {
            string status = SolicitarStatus();

            if (status == null)
                return;

            ExibirTarefas(projeto.TarefasPorStatus(status));
        }
        else if (opcao == 2)
        {
            int prioridade = SolicitarPrioridade();

            if (prioridade == 0)
                return;

            ExibirTarefas(projeto.TarefasPorPrioridade(prioridade));
        }
        else
        {
            Console.WriteLine("Opção inválida.");
        }
    }

    static void FiltrarTarefasTodosProjetos()
    {
        Console.WriteLine("1. Filtrar por status");
        Console.WriteLine("2. Filtrar por prioridade");
        Console.Write("Escolha: ");

        if (!int.TryParse(Console.ReadLine(), out int opcao))
        {
            Console.WriteLine("Opção inválida.");
            return;
        }

        List<Tarefa> tarefas = new List<Tarefa>();

        if (opcao == 1)
        {
            string status = SolicitarStatus();

            if (status == null)
                return;

            foreach (Projeto p in projetos.Listar())
            {
                Console.WriteLine($"\nProjeto: {p.Nome}");
                List<Tarefa> filtradas = p.TarefasPorStatus(status);
                ExibirTarefas(filtradas);
                tarefas.AddRange(filtradas);
            }
        }
        else if (opcao == 2)
        {
            int prioridade = SolicitarPrioridade();

            if (prioridade == 0)
                return;

            foreach (Projeto p in projetos.Listar())
            {
                Console.WriteLine($"\nProjeto: {p.Nome}");
                List<Tarefa> filtradas = p.TarefasPorPrioridade(prioridade);
                ExibirTarefas(filtradas);
                tarefas.AddRange(filtradas);
            }
        }
        else
        {
            Console.WriteLine("Opção inválida.");
            return;
        }

        Console.WriteLine($"\nTotal de tarefas encontradas: {tarefas.Count}");
    }

    static string SolicitarStatus()
    {
        Console.WriteLine("1. Aberta");
        Console.WriteLine("2. Fechada");
        Console.WriteLine("3. Cancelada");
        Console.Write("Escolha o status: ");

        if (!int.TryParse(Console.ReadLine(), out int opcao))
        {
            Console.WriteLine("Opção inválida.");
            return null;
        }

        switch (opcao)
        {
            case 1: return "Aberta";
            case 2: return "Fechada";
            case 3: return "Cancelada";
            default:
                Console.WriteLine("Status inválido.");
                return null;
        }
    }

    static int SolicitarPrioridade()
    {
        Console.WriteLine("1. Alta");
        Console.WriteLine("2. Média");
        Console.WriteLine("3. Baixa");
        Console.Write("Escolha a prioridade: ");

        if (!int.TryParse(Console.ReadLine(), out int prioridade) || prioridade < 1 || prioridade > 3)
        {
            Console.WriteLine("Prioridade inválida.");
            return 0;
        }

        return prioridade;
    }

    static void ResumoGeral()
    {
        List<Projeto> lista = projetos.Listar();
        List<Tarefa> tarefas = lista.SelectMany(p => p.Tarefas).ToList();

        int abertas = tarefas.Count(t => t.Status == "Aberta");
        int fechadas = tarefas.Count(t => t.Status == "Fechada");
        int canceladas = tarefas.Count(t => t.Status == "Cancelada");

        // Corrigido: porcentagem em relação ao total de tarefas
        double percentual = tarefas.Count > 0 ? (double)fechadas / tarefas.Count * 100 : 0;

        Console.WriteLine("===== RESUMO GERAL =====");
        Console.WriteLine($"Quantidade de projetos: {lista.Count}");
        Console.WriteLine($"Quantidade total de tarefas: {tarefas.Count}");
        Console.WriteLine($"Tarefas abertas: {abertas}");
        Console.WriteLine($"Tarefas fechadas: {fechadas}");
        Console.WriteLine($"Tarefas canceladas: {canceladas}");
        Console.WriteLine($"Percentual de concluídas em relação ao total: {percentual:F2}%");
    }

    static void ExibirTarefas(List<Tarefa> tarefas)
    {
        if (tarefas.Count == 0)
        {
            Console.WriteLine("Nenhuma tarefa encontrada.");
            return;
        }

        foreach (Tarefa t in tarefas)
        {
            string prioridade;
            switch (t.Prioridade)
            {
                case 1:
                    prioridade = "Alta";
                    break;
                case 2:
                    prioridade = "Média";
                    break;
                case 3:
                    prioridade = "Baixa";
                    break;
                default:
                    prioridade = "Desconhecida";
                    break;
            }

            Console.WriteLine("----------------------------------------");
            Console.WriteLine($"ID: {t.Id}");
            Console.WriteLine($"Título: {t.Titulo}");
            Console.WriteLine($"Descrição: {t.Descricao}");
            Console.WriteLine($"Prioridade: {prioridade}");
            Console.WriteLine($"Status: {t.Status}");
            Console.WriteLine($"Data de criação: {t.DataCriacao:dd/MM/yyyy HH:mm}");

            if (t.DataConclusao.HasValue)
                Console.WriteLine($"Data de conclusão: {t.DataConclusao.Value:dd/MM/yyyy HH:mm}");
            else
                Console.WriteLine("Data de conclusão: Não concluída");
        }

        Console.WriteLine("----------------------------------------");
    }
}
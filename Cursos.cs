using System;

namespace GestaoEscolar
{
    public class Aluno
    {
        private int id;
        private string nome;
        private Disciplina[] disciplinas;

        public int Id { => id; set => id = value; }
        public string Nome { => nome; set => nome = value; }
        public Disciplina[] Disciplinas => disciplinas;

        public Aluno(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
            this.disciplinas = new Disciplina[6];
        }

        public Aluno(int id) : this(id, "") { }

        public bool Matricular(Disciplina d)
        {
            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] == null)
                {
                    disciplinas[i] = d;
                    return true;
                }
            }
            return false;
        }

        public bool Desmatricular(Disciplina d)
        {
            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] != null && disciplinas[i].Equals(d))
                {
                    disciplinas[i] = null;
                    return true;
                }
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Aluno outro)
            {
                return this.id == outro.id;
            }
            return false;
        }
    }

    public class Disciplina
    {
        private int id;
        private string descricao;
        private Aluno[] alunos;

        public int Id { => id; set => id = value; }
        public string Descricao { => descricao; set => descricao = value; }
        public Aluno[] Alunos => alunos;

        public Disciplina(int id, string descricao)
        {
            this.id = id;
            this.descricao = descricao;
            this.alunos = new Aluno[15];
        }

        public Disciplina(int id) : this(id, "") { }

        public bool MatricularAluno(Aluno a)
        {
            if (PesquisarAluno(a)) return false;

            for (int i = 0; i < alunos.Length; i++)
            {
                if (alunos[i] == null)
                {
                    alunos[i] = a;
                    a.Matricular(this);
                    return true;
                }
            }
            return false;
        }

        public bool DesmatricularAluno(Aluno a)
        {
            for (int i = 0; i < alunos.Length; i++)
            {
                if (alunos[i] != null && alunos[i].Equals(a))
                {
                    alunos[i].Desmatricular(this);
                    alunos[i] = null;
                    return true;
                }
            }
            return false;
        }

        public bool PesquisarAluno(Aluno a)
        {
            if (a == null) return false;
            foreach (var aluno in alunos)
            {
                if (aluno != null && aluno.Equals(a))
                    return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is Disciplina outra)
            {
                return this.id == outra.id;
            }
            return false;
        }
    }

    public class Escola
    {
        private Disciplina[] disciplinas;

        public Disciplina[] Disciplinas => disciplinas;

        public Escola()
        {
            disciplinas = new Disciplina[12];
        }

        public bool AdicionarDisciplina(Disciplina d)
        {
            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] == null)
                {
                    disciplinas[i] = d;
                    return true;
                }
            }
            return false;
        }

        public Disciplina PesquisarDisciplina(int id)
        {
            foreach (var d in disciplinas)
            {
                if (d != null && d.Id == id)
                    return d;
            }
            return null;
        }

        public bool RemoverDisciplina(Disciplina d)
        {
            for (int i = 0; i < disciplinas.Length; i++)
            {
                if (disciplinas[i] != null && disciplinas[i].Equals(d))
                {
                    if (disciplinas[i].Alunos != null)
                    {
                        foreach (var aluno in disciplinas[i].Alunos)
                        {
                            if (aluno != null)
                            {
                                return false; 
                            }
                        }
                    }
                    disciplinas[i] = null;
                    return true;
                }
            }
            return false;
        }

        public Aluno PesquisarAluno(int id)
        {
            foreach (var d in disciplinas)
            {
                if (d != null && d.Alunos != null)
                {
                    foreach (var a in d.Alunos)
                    {
                        if (a != null && a.Id == id)
                            return a;
                    }
                }
            }
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Escola escola = new Escola();
            int opcao = -1;

            while (opcao != 0)
            {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("0. Sair");
                Console.WriteLine("1. Adicionar disciplina");
                Console.WriteLine("2. Pesquisar disciplina (mostrar dados e alunos)");
                Console.WriteLine("3. Remover disciplina");
                Console.WriteLine("4. Pesquisar aluno (mostrar disciplinas)");
                Console.WriteLine("5. Matricular aluno em disciplina");
                Console.WriteLine("6. Desmatricular aluno de disciplina");
                Console.Write("Escolha uma opção: ");

                if (!int.TryParse(Console.ReadLine(), out opcao))
                    continue;

                switch (opcao)
                {
                    case 1:
                        Console.Write("ID da disciplina: ");
                        int idDisc = int.Parse(Console.ReadLine());
                        Console.Write("Descrição da disciplina: ");
                        string descDisc = Console.ReadLine();

                        if (escola.AdicionarDisciplina(new Disciplina(idDisc, descDisc)))
                            Console.WriteLine("Disciplina adicionada com sucesso!");
                        else
                            Console.WriteLine("Não foi possível adicionar a disciplina.");
                        break;

                    case 2:
                        Console.Write("ID da disciplina a pesquisar: ");
                        int idPesq = int.Parse(Console.ReadLine());
                        Disciplina dEncontrada = escola.PesquisarDisciplina(idPesq);

                        if (dEncontrada != null)
                        {
                            Console.WriteLine($"Disciplina: {dEncontrada.Id} - {dEncontrada.Descricao}");
                            Console.WriteLine("Alunos matriculados:");
                            foreach (var a in dEncontrada.Alunos)
                            {
                                if (a != null)
                                    Console.WriteLine($" - ID: {a.Id}, Nome: {a.Nome}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Disciplina não encontrada.");
                        }
                        break;

                    case 3:
                        Console.Write("ID da disciplina a remover: ");
                        int idRem = int.Parse(Console.ReadLine());
                        Disciplina dRem = escola.PesquisarDisciplina(idRem);

                        if (dRem != null && escola.RemoverDisciplina(dRem))
                            Console.WriteLine("Disciplina removida com sucesso!");
                        else
                            Console.WriteLine("Não foi possível remover a disciplina (pode possuir alunos ou não existir).");
                        break;

                    case 4:
                        Console.Write("ID do aluno a pesquisar: ");
                        int idAlunoPesq = int.Parse(Console.ReadLine());
                        Aluno aEncontrado = escola.PesquisarAluno(idAlunoPesq);

                        if (aEncontrado != null)
                        {
                            Console.WriteLine($"Aluno: {aEncontrado.Id} - {aEncontrado.Nome}");
                            Console.WriteLine("Disciplinas matriculadas:");
                            foreach (var d in aEncontrado.Disciplinas)
                            {
                                if (d != null)
                                    Console.WriteLine($" - ID: {d.Id}, Descrição: {d.Descricao}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Aluno não encontrado.");
                        }
                        break;

                    case 5:
                        Console.Write("ID da disciplina: ");
                        int idDiscMat = int.Parse(Console.ReadLine());
                        Disciplina discMat = escola.PesquisarDisciplina(idDiscMat);

                        if (discMat == null)
                        {
                            Console.WriteLine("Disciplina não encontrada.");
                            break;
                        }

                        Console.Write("ID do aluno: ");
                        int idAlunoMat = int.Parse(Console.ReadLine());
                        Aluno alunoMat = escola.PesquisarAluno(idAlunoMat);

                        if (alunoMat == null)
                        {
                            Console.Write("Nome do aluno: ");
                            string nomeAlunoMat = Console.ReadLine();
                            alunoMat = new Aluno(idAlunoMat, nomeAlunoMat);
                        }

                        if (discMat.MatricularAluno(alunoMat))
                            Console.WriteLine("Aluno matriculado com sucesso!");
                        else
                            Console.WriteLine("Não foi possível matricular o aluno.");
                        break;

                    case 6:
                        Console.Write("ID da disciplina: ");
                        int idDiscRem = int.Parse(Console.ReadLine());
                        Disciplina discRemAluno = escola.PesquisarDisciplina(idDiscRem);

                        if (discRemAluno == null)
                        {
                            Console.WriteLine("Disciplina não encontrada.");
                            break;
                        }

                        Console.Write("ID do aluno: ");
                        int idAlunoRem = int.Parse(Console.ReadLine());
                        Aluno alunoRem = escola.PesquisarAluno(idAlunoRem);

                        if (alunoRem == null)
                        {
                            Console.WriteLine("Aluno não encontrado.");
                            break;
                        }

                        if (discRemAluno.DesmatricularAluno(alunoRem))
                            Console.WriteLine("Aluno desmatriculado com sucesso!");
                        else
                            Console.WriteLine("Aluno não encontrado nesta disciplina.");
                        break;
                }
            }
        }
    }
}

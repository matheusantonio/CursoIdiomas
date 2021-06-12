using CursoIdiomas.API.Entities.Notification;
using CursoIdiomas.API.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CursoIdiomas.API.Entities
{
    public class Turma : Notificavel
    {
        public int Numero { get; }
        public List<Aluno> Alunos { get; }
        public Idioma Idioma { get; set; }

        public Turma(Idioma idioma)
        {
            Idioma = idioma;
            Alunos = new List<Aluno>();
        }

        public Turma(int numero, Idioma idioma, List<Aluno> alunos)
        {
            Numero = numero;
            Idioma = idioma;
            Alunos = alunos;
        }

        public void AdicionarAluno(Aluno aluno)
        {
            if (Alunos.Where(a => a.Matricula == aluno.Matricula)
                    .FirstOrDefault() != null)
            {
                AdicionarNotificacao("O aluno já está na turma");
            } else if(Alunos.Count < 5)
            {
                Alunos.Add(aluno);
            } else
            {
                AdicionarNotificacao("A turma está cheia");
            }
        }
        public void RemoverAluno(Aluno aluno)
        {
            var result = Alunos.Where(a => a.Matricula == aluno.Matricula).FirstOrDefault();
            if(result != null)
            {
                Alunos.RemoveAll(a => a.Matricula == aluno.Matricula);
            } else
            {
                AdicionarNotificacao("O aluno não está associado a essa turma");
            }
        }

        public bool TemAlunos()
        {
            return Alunos.Count > 0;
        }
    }
}

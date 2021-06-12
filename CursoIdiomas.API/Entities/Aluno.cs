using CursoIdiomas.API.Entities.Notification;
using CursoIdiomas.API.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Entities
{
    public class Aluno : Notificavel
    {
        public int Matricula { get; }
        public Nome Nome { get; set; }
        public Email Email { get; set; }

        public Aluno(int matricula, Nome nome, Email email)
        {
            Matricula = matricula;
            Nome = nome;
            Email = email;
        }
    }
}

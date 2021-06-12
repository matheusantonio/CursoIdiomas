using CursoIdiomas.API.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Entities
{
    public class Idioma : Notificavel
    {
        public string Nome { get; private set; }

        public Idioma(string nome)
        {
            Nome = nome;
            if(nome.Length == 0)
            {
                AdicionarNotificacao("Nome do idioma inválido");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Entities.Notification
{
    public class Notificavel
    {
        public List<string> Notificacoes { get; }

        public Notificavel()
        {
            Notificacoes = new List<string>();
        }

        public void AdicionarNotificacao(string notificacao)
        {
            Notificacoes.Add(notificacao);
        }

        public bool Invalido()
        {
            return Notificacoes.Count > 0;
        }
    }
}

using CursoIdiomas.API.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Entities.ValueObjects
{
    public class Nome : Notificavel
    {
        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }

        public Nome(string primeiroNome, string sobrenome)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;
        }
    }
}

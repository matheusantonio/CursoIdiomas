using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CursoIdiomas.API.Entities.Notification;

namespace CursoIdiomas.API.Entities.ValueObjects
{
    public class Email : Notificavel
    {
        public string Endereco { get; private set; }

        public Email(string endereco)
        {
            Endereco = endereco;
            var regexValidarEmail = @"^[a-z0-9.]+@[a-z0-9]+\.[a-z]+(\.[a-z]+)?$";
            if(!Regex.IsMatch(Endereco, regexValidarEmail))
            {
                AdicionarNotificacao("Formato do Email está inválido");
            }
        }

    }
}

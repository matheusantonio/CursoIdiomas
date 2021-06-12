using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class RegistrarUsuarioCommand : ICommand
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class CriarTurmaCommand : ICommand
    {
        public string Idioma { get; set; }
    }
}

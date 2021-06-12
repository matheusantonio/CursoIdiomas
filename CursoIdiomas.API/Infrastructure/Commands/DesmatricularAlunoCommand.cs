using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class DesmatricularAlunoCommand : ICommand
    {
        public int MatriculaAluno { get; set; }
        public int NumeroTurma { get; set; }
    }
}

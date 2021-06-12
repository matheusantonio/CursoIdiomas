using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class CriarAlunoCommand : ICommand
    {
        public int Matricula { get; set; }
        public string PrimeiroNome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public int IdTurma { get; set; }
    }
}

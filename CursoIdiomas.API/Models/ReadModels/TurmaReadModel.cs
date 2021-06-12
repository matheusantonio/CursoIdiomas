using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Models.ReadModels
{
    public class AlunoTurmaReadModel
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
    }
    public class TurmaReadModel
    {
        public int Numero { get; set; }
        public List<AlunoTurmaReadModel> Alunos { get; set; }
        public string Idioma { get; set; }
    }
}

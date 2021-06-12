using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.APIs.Infrastructure.Models.ReadModels
{
    public class TurmaAlunoReadModel
    {
        public int Numero { get; set; }
        public string Idioma { get; set; }
    }
    public class AlunoReadModel
    {
        public int Matricula { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public List<TurmaAlunoReadModel> Turmas { get; set; }
    }
}

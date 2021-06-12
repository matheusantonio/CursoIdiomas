using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Persistence.Models
{
    [Table("AlunoTurma")]
    public class AlunoTurmaModel
    {
        public int Matricula { get; set; }
        public AlunoModel Aluno { get; set; }

        public int Numero { get; set; }
        public TurmaModel Turma { get; set; }
    }
}

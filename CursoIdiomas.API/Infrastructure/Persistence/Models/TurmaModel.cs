using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Persistence.Models
{
    [Table("Turma")]
    public class TurmaModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Numero { get; set; }

        [ForeignKey("IdIdioma")]
        public IdiomaModel Idioma { get; set; }

        public List<AlunoTurmaModel> Alunos { get; set; }
    }
}

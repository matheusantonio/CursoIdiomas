using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Persistence.Models
{
    [Table("Usuario")]
    public class UsuarioModel
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string HashSenha { get; set; }
    }
}

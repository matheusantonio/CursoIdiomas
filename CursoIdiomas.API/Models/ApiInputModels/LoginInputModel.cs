using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Models.ApiInputModels
{
    public class LoginInputModel
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
}

using CursoIdiomas.API.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Authentication
{
    public interface ITokenService
    {
        public string GerarToken(UsuarioModel usuario);
    }
}

using CursoIdiomas.API.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Repositories
{
    public interface IUsuarioRepository
    {
        public Task<UsuarioModel> RecuperarUsuario(string nome);
        public Task<UsuarioModel> VerificarUsuario(string nome, string senha);
        public UsuarioModel CriarUsuario(string nome, string senha);
    }
}

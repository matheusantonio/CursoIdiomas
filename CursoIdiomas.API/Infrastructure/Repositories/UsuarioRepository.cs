using CursoIdiomas.API.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using CursoIdiomas.API.Infrastructure.Persistence;

namespace CursoIdiomas.API.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CursoIdiomasContext _context;

        public UsuarioRepository(CursoIdiomasContext context)
        {
            _context = context;
        }

        public UsuarioModel CriarUsuario(string nome, string senha)
        {
            var hashSenha = BCrypt.Net.BCrypt.HashPassword(senha);

            var novoUsuario = new UsuarioModel 
            { 
                Nome = nome, 
                HashSenha = hashSenha
            };

            _context.Usuarios.Add(novoUsuario);

            return novoUsuario;
        }

        public async Task<UsuarioModel> RecuperarUsuario(string nome)
        {
            var usuario = await (from u in _context.Usuarios
                                 where u.Nome == nome
                                 select u).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<UsuarioModel> VerificarUsuario(string nome, string senha)
        {
            var usuario = await RecuperarUsuario(nome);
            
            if (usuario != null &&
                    BCrypt.Net.BCrypt.Verify(senha, usuario.HashSenha))
            {
                return usuario;
            }

            return null;
        }
    }
}

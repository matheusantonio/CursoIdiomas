using CursoIdiomas.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        public IAlunoRepository AlunoRepository { get; }
        public ITurmaRepository TurmaRepository { get; }
        public IUsuarioRepository UsuarioRepository { get; }
        public Task Salvar();
        public Task Descartar();
    }
}

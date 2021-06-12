using CursoIdiomas.API.Infrastructure.Persistence;
using CursoIdiomas.API.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IAlunoRepository _alunoRepository { get; set; }
        private ITurmaRepository _turmaRepository { get; set; }
        private IUsuarioRepository _usuarioRepository { get; set; }

        private readonly CursoIdiomasContext _context;
        public UnitOfWork(CursoIdiomasContext context)
        {
            _context = context;
        }

        public IAlunoRepository AlunoRepository
        {
            get
            {
                if (_alunoRepository == null) {
                    _alunoRepository = new AlunoRepository(_context);
                }
                return _alunoRepository;
            }
        }

        public ITurmaRepository TurmaRepository
        {
            get
            {
                if(_turmaRepository == null)
                {
                    _turmaRepository = new TurmaRepository(_context);
                }
                return _turmaRepository;
            }
        }

        public IUsuarioRepository UsuarioRepository
        {
            get
            {
                if(_usuarioRepository == null)
                {
                    _usuarioRepository = new UsuarioRepository(_context);
                }
                return _usuarioRepository;
            }
        }

        public async Task Salvar()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Descartar()
        {
            await _context.DisposeAsync();
        }
    }
}

using CursoIdiomas.API.Entities;
using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Repositories
{
    public interface ITurmaRepository
    {
        public Task<List<Turma>> BuscarTodasAsTurmas();
        public Task<List<Turma>> BuscarTurmasDeAluno(int matricula);
        public Task<Turma> BuscarTurma(int Numero);
        public Task<Turma> CriarTurma(Turma turma);
        public Task<Turma> AlterarTurma(Turma turma);
        public Task RemoverTurma(int numero);
    }
}

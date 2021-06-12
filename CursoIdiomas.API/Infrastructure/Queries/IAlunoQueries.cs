using CursoIdiomas.APIs.Infrastructure.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Queries
{
    public interface IAlunoQueries
    {
        public Task<AlunoReadModel> BuscarAluno(int matricula);
        public Task<List<AlunoReadModel>> BuscarTodosAlunos();
    }
}

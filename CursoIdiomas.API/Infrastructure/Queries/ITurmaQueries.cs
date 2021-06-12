using CursoIdiomas.API.Infrastructure.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Queries
{
    public interface ITurmaQueries
    {
        public Task<TurmaReadModel> BuscarTurma(int numero);
        public Task<List<TurmaReadModel>> BuscarTodasTurmas();
    }
}

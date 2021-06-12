using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Commands.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        public Task<ICommandResult> Handle(T command);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands.Results
{
    public interface ICommandResult
    {
        public bool Success { get; }
    }
}

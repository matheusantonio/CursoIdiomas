using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands.Results
{
    public class CommandResult : ICommandResult
    {
        public bool Success { get; }
        public string Message { get; set; }

        public CommandResult()
        {

        }

        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}

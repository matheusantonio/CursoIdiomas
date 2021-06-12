using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands.Results
{
    public class CreatedCommandResult : CommandResult
    {
        public object Id { get; set; }

        public CreatedCommandResult(bool success, string message, object id) : base(success, message)
        {
            Id = id;
        }
    }
}

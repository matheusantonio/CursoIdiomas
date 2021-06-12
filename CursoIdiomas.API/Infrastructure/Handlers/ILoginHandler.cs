using CursoIdiomas.API.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Handlers
{
    public interface ILoginHandler :
        IHandler<RegistrarUsuarioCommand>
    {
    }
}

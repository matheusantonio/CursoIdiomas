using CursoIdiomas.API.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Handlers
{
    public interface IAlunoHandler :
        IHandler<CriarAlunoCommand>,
        IHandler<DesmatricularAlunoCommand>,
        IHandler<MatricularAlunoCommand>,
        IHandler<AlterarAlunoCommand>,
        IHandler<RemoverAlunoCommand>
    {
    }
}

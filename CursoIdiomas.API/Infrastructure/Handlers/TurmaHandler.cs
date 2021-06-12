using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Commands.Results;
using CursoIdiomas.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoIdiomas.API.Infrastructure.Repositories;
using CursoIdiomas.API.Entities.ValueObjects;
using CursoIdiomas.API.Infrastructure.UnitOfWork;

namespace CursoIdiomas.API.Infrastructure.Handlers
{
    public class TurmaHandler : 
        ITurmaHandler
        
    {
        private readonly IUnitOfWork _unitOfWork;

        public TurmaHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(CriarTurmaCommand command)
        {
            var idioma = new Idioma(command.Idioma);

            if (idioma.Invalido())
                return new CommandResult(false, "Idioma informado inválido");

            var turma = new Turma(idioma);

            turma = await _unitOfWork.TurmaRepository.CriarTurma(turma);
            await _unitOfWork.Salvar();

            return new CreatedCommandResult(true, "Turma criada com sucesso", turma.Numero);
        }

        public async Task<ICommandResult> Handle(AlterarTurmaCommand command)
        {
            var turma = await _unitOfWork.TurmaRepository.BuscarTurma(command.Numero);
            if (turma == null)
                return new CommandResult(false, "Turma informada não existe");

            var idioma = new Idioma(command.Idioma);
            if (idioma.Invalido())
                return new CommandResult(false, "Idioma informado inválido");

            turma.Idioma = idioma;

            await _unitOfWork.TurmaRepository.AlterarTurma(turma);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Turma atualizada com sucesso");
        }

        public async Task<ICommandResult> Handle(RemoverTurmaCommand command)
        {
            var turma = await _unitOfWork.TurmaRepository.BuscarTurma(command.Numero);
            if (turma == null)
                return new CommandResult(false, "A turma infomada não existe");

            if (turma.TemAlunos())
                return new CommandResult(false, "A turma ainda possui alunos");

            await _unitOfWork.TurmaRepository.RemoverTurma(turma.Numero);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Turma removida com sucesso");
        }
    }
}

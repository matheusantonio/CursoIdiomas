using CursoIdiomas.API.Entities;
using CursoIdiomas.API.Entities.ValueObjects;
using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Commands.Results;
using CursoIdiomas.API.Infrastructure.Repositories;
using CursoIdiomas.API.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Handlers
{
    public class AlunoHandler :
        IAlunoHandler
        
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlunoHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(CriarAlunoCommand command)
        {
            var alunoExistente = await _unitOfWork.AlunoRepository.BuscarAluno(command.Matricula);
            if (alunoExistente != null)
                return new CommandResult(false, "Já existe um aluno com essa matrícula");

            var nome = new Nome(command.PrimeiroNome, command.Sobrenome);
            if (nome.Invalido())
                return new CommandResult(false, "Nome inválido");

            var email = new Email(command.Email);
            if (email.Invalido())
                return new CommandResult(false, "Erro ao cadastrar Email");

            var novoAluno = new Aluno(command.Matricula, nome, email);
            if (novoAluno.Invalido())
                return new CommandResult(false, "Erro ao matricular aluno");

            var turma = await _unitOfWork.TurmaRepository.BuscarTurma(command.IdTurma);
            if (turma == null)
                return new CommandResult(false, "Turma inválida. Aluno deve ser cadastrado com turma");

            turma.AdicionarAluno(novoAluno);
            if (turma.Invalido())
                return new CommandResult(false, "A turma está cheia");

            await _unitOfWork.AlunoRepository.CriarAluno(novoAluno, turma);
            await _unitOfWork.Salvar();

            return new CreatedCommandResult(true, "Aluno matriculado com sucesso", novoAluno.Matricula);
        }

        public async Task<ICommandResult> Handle(DesmatricularAlunoCommand command)
        {
            var aluno = await _unitOfWork.AlunoRepository.BuscarAluno(command.MatriculaAluno);
            if (aluno == null)
                return new CommandResult(false, "Aluno não encontrado");
            var turma = await _unitOfWork.TurmaRepository.BuscarTurma(command.NumeroTurma);
            if (turma == null)
                return new CommandResult(false, "Turma não encontrada");

            turma.RemoverAluno(aluno);
            if (turma.Invalido())
                return new CommandResult(false, "Erro ao retirar aluno da turma");

            await _unitOfWork.AlunoRepository.DesmatricularAluno(aluno, turma);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Aluno desmatriculado com sucesso");
        }

        public async Task<ICommandResult> Handle(MatricularAlunoCommand command)
        {
            var aluno = await _unitOfWork.AlunoRepository.BuscarAluno(command.MatriculaAluno);
            if (aluno == null)
                return new CommandResult(false, "Aluno não encontrado");
            var turma = await _unitOfWork.TurmaRepository.BuscarTurma(command.NumeroTurma);
            if (turma == null)
                return new CommandResult(false, "Turma não encontrada");

            turma.AdicionarAluno(aluno);
            if (turma.Invalido())
                return new CommandResult(false, "Erro ao matricular aluno na turma");

            await _unitOfWork.AlunoRepository.MatricularAluno(aluno, turma);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Aluno matriculado com sucesso na turma");
        }

        public async Task<ICommandResult> Handle(AlterarAlunoCommand command)
        {
            var aluno = await _unitOfWork.AlunoRepository.BuscarAluno(command.Matricula);
            if (aluno == null)
                return new CommandResult(false, "Aluno não encontrado");

            var nome = new Nome(command.PrimeiroNome, command.Sobrenome);
            if (nome.Invalido())
                return new CommandResult(false, "Nome inválido");

            var email = new Email(command.Email);
            if (email.Invalido())
                return new CommandResult(false, "Email inválido");

            aluno.Nome = nome;
            aluno.Email = email;

            await _unitOfWork.AlunoRepository.AlterarAluno(aluno);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Aluno alterado com sucesso");
        }

        public async Task<ICommandResult> Handle(RemoverAlunoCommand command)
        {
            var aluno = await _unitOfWork.AlunoRepository.BuscarAluno(command.Matricula);
            if (aluno == null)
                return new CommandResult(false, "Aluno não encontrado");

            var turmasDoAluno = await _unitOfWork.TurmaRepository.BuscarTurmasDeAluno(aluno.Matricula);
            if (turmasDoAluno.Count > 0)
                return new CommandResult(false, "O aluno ainda está matriculado em alguma turma");

            await _unitOfWork.AlunoRepository.RemoverAluno(aluno.Matricula);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Aluno removido com sucesso");
        }
    }
}

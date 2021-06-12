using CursoIdiomas.API.Infrastructure.UnitOfWork;
using CursoIdiomas.APIs.Infrastructure.Models.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Queries
{
    public class AlunoQueries : IAlunoQueries
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlunoQueries(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AlunoReadModel> BuscarAluno(int matricula)
        {
            var aluno = await _unitOfWork.AlunoRepository.BuscarAluno(matricula);

            return new AlunoReadModel
            {
                Matricula = aluno.Matricula,
                Nome = aluno.Nome.PrimeiroNome,
                Sobrenome = aluno.Nome.Sobrenome,
                Email = aluno.Email.Endereco,
                Turmas = await ListarTurmas(matricula)
            };
        }

        public async Task<List<AlunoReadModel>> BuscarTodosAlunos()
        {
            var alunos = await _unitOfWork.AlunoRepository.BuscarTodosOsAlunos();

            var alunosResult = new List<AlunoReadModel>();

            foreach(var aluno in alunos)
            {
                var turmas = await ListarTurmas(aluno.Matricula);

                alunosResult.Add(
                    new AlunoReadModel
                    {
                        Matricula = aluno.Matricula,
                        Nome = aluno.Nome.PrimeiroNome,
                        Sobrenome = aluno.Nome.Sobrenome,
                        Email = aluno.Email.Endereco,
                        Turmas = turmas
                    });
            }

            return alunosResult;
        }

        private async Task<List<TurmaAlunoReadModel>> ListarTurmas(int matricula)
        {
            var turmas = await _unitOfWork.TurmaRepository.BuscarTurmasDeAluno(matricula);

            var turmasAlunoResult = new List<TurmaAlunoReadModel>();

            turmas.ForEach(turma =>
            {
                turmasAlunoResult.Add(
                    new TurmaAlunoReadModel
                    {
                        Numero = turma.Numero,
                        Idioma = turma.Idioma.Nome
                    });
            });

            return turmasAlunoResult;
        }
    }
}

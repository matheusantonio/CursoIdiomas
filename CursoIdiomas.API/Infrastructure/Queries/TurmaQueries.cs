using CursoIdiomas.API.Entities;
using CursoIdiomas.API.Infrastructure.Models.ReadModels;
using CursoIdiomas.API.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Queries
{
    public class TurmaQueries : ITurmaQueries
    {
        private readonly IUnitOfWork _unitOfWork;

        public TurmaQueries(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TurmaReadModel>> BuscarTodasTurmas()
        {
            var turmas = await _unitOfWork.TurmaRepository.BuscarTodasAsTurmas();

            var turmasResult = new List<TurmaReadModel>();

            turmas.ForEach(turma =>
            {
                turmasResult.Add(
                    new TurmaReadModel
                    {
                        Numero = turma.Numero,
                        Idioma = turma.Idioma.Nome,
                        Alunos = ListarAlunos(turma)
                    });
            });

            return turmasResult;
        }

        public async Task<TurmaReadModel> BuscarTurma(int numero)
        {
            var turma = await _unitOfWork.TurmaRepository.BuscarTurma(numero);

            return new TurmaReadModel
            {
                Numero = turma.Numero,
                Idioma = turma.Idioma.Nome,
                Alunos = ListarAlunos(turma)
            };
        }

        private List<AlunoTurmaReadModel> ListarAlunos(Turma turma)
        {
            var alunos = new List<AlunoTurmaReadModel>();

            turma.Alunos.ForEach(aluno =>
            {
                alunos.Add(
                    new AlunoTurmaReadModel
                    {
                        Matricula = aluno.Matricula,
                        Nome = aluno.Nome.PrimeiroNome,
                        Sobrenome = aluno.Nome.Sobrenome,
                        Email = aluno.Email.Endereco
                    });
            });

            return alunos;
        }
    }
}

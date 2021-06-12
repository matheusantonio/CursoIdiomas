using CursoIdiomas.API.Entities;
using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Repositories
{
    public interface IAlunoRepository
    {
        public Task<List<Aluno>> BuscarTodosOsAlunos();
        public Task<Aluno> BuscarAluno(int matricula);
        public Task<Aluno> CriarAluno(Aluno aluno, Turma turma);
        public Task DesmatricularAluno(Aluno aluno, Turma turma);
        public Task MatricularAluno(Aluno aluno, Turma turma);
        public Task<Aluno> AlterarAluno(Aluno aluno);
        public Task RemoverAluno(int matricula);
    }
}

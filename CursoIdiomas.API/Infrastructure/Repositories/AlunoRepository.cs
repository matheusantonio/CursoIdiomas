using CursoIdiomas.API.Entities;
using CursoIdiomas.API.Entities.ValueObjects;
using CursoIdiomas.API.Infrastructure.Persistence;
using CursoIdiomas.API.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Repositories
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly CursoIdiomasContext _context;

        public AlunoRepository(CursoIdiomasContext context)
        {
            _context = context;
        }

        public async Task<Aluno> AlterarAluno(Aluno aluno)
        {
            var alunoAtualizado = await _context.Alunos
                                    .Include(a => a.Turmas)
                                    .ThenInclude(at => at.Turma)
                                    .Where(a => a.Matricula == aluno.Matricula)
                                    .FirstOrDefaultAsync();

            alunoAtualizado.PrimeiroNome = aluno.Nome.PrimeiroNome;
            alunoAtualizado.Sobrenome = aluno.Nome.Sobrenome;
            alunoAtualizado.Email = aluno.Email.Endereco;

            _context.Update(alunoAtualizado);

            return new Aluno(
                alunoAtualizado.Matricula,
                new Nome(alunoAtualizado.PrimeiroNome, alunoAtualizado.Sobrenome),
                new Email(alunoAtualizado.Email));
        }

        public async Task MatricularAluno(Aluno aluno, Turma turma)
        {
            var alunoEncontrado = await _context.Alunos
                                    .Include(a => a.Turmas)
                                    .ThenInclude(at => at.Turma)
                                    .Where(a => a.Matricula == aluno.Matricula)
                                    .FirstOrDefaultAsync();
            var turmaEncontrada = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .ThenInclude(at => at.Aluno)
                                        .Where(t => t.Numero == turma.Numero)
                                        .FirstOrDefaultAsync();

            var alunoTurma = new AlunoTurmaModel
            {
                Aluno = alunoEncontrado,
                Matricula = alunoEncontrado.Matricula,
                Turma = turmaEncontrada,
                Numero = turmaEncontrada.Numero
            };

            turmaEncontrada.Alunos.Add(alunoTurma);
            alunoEncontrado.Turmas.Add(alunoTurma);
            _context.Update(alunoEncontrado);
            _context.Update(turmaEncontrada);
        }

        public async Task<Aluno> BuscarAluno(int matricula)
        {
            var aluno = await _context.Alunos
                                    .Include(a => a.Turmas)
                                    .ThenInclude(at => at.Turma)
                                    .Where(a => a.Matricula == matricula)
                                    .FirstOrDefaultAsync();

            if (aluno == null) return null;

            return new Aluno(
                aluno.Matricula,
                new Nome(aluno.PrimeiroNome, aluno.Sobrenome),
                new Email(aluno.Email));
        }

        public async Task<List<Aluno>> BuscarTodosOsAlunos()
        {
            var alunosEncontrados = await _context.Alunos
                                        .Include(a => a.Turmas)
                                        .ThenInclude(at => at.Turma)
                                        .ToListAsync();

            var alunos = new List<Aluno>();

            alunosEncontrados.ForEach(aluno =>
            {
                alunos.Add(
                    new Aluno(
                        aluno.Matricula,
                        new Nome(aluno.PrimeiroNome, aluno.Sobrenome),
                        new Email(aluno.Email)));
            });

            return alunos;
        }

        public async Task DesmatricularAluno(Aluno aluno, Turma turma)
        {
            var alunoEncontrado = await _context.Alunos
                                    .Include(a => a.Turmas)
                                    .Where(a => a.Matricula == aluno.Matricula)
                                    .FirstOrDefaultAsync();
            var turmaEncontrada = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .Where(t => t.Numero == turma.Numero)
                                        .FirstOrDefaultAsync();

            var alunoTurma = new AlunoTurmaModel
            {
                Aluno = alunoEncontrado,
                Matricula = alunoEncontrado.Matricula,
                Turma = turmaEncontrada,
                Numero = turmaEncontrada.Numero
            };

            turmaEncontrada.Alunos.RemoveAll(a => a.Matricula == alunoEncontrado.Matricula);
            alunoEncontrado.Turmas.RemoveAll(t => t.Numero == turmaEncontrada.Numero);
            _context.Update(alunoEncontrado);
            _context.Update(turmaEncontrada);
            //_context.Remove(alunoTurma);
        }

        public async Task<Aluno> CriarAluno(Aluno aluno, Turma turma)
        {
            var novoAluno = new AlunoModel
            {
                Matricula = aluno.Matricula,
                PrimeiroNome = aluno.Nome.PrimeiroNome,
                Sobrenome = aluno.Nome.Sobrenome,
                Email = aluno.Email.Endereco,
                Turmas = new List<AlunoTurmaModel>()
            };

            var turmaEncontrada = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .ThenInclude(at => at.Aluno)
                                        .Where(t => t.Numero == turma.Numero)
                                        .FirstOrDefaultAsync();

            var alunoTurma = new AlunoTurmaModel
            {
                Aluno = novoAluno,
                Matricula = novoAluno.Matricula,
                Turma = turmaEncontrada,
                Numero = turmaEncontrada.Numero
            };

            novoAluno.Turmas.Add(alunoTurma);
            turmaEncontrada.Alunos.Add(alunoTurma);

            _context.Alunos.Add(novoAluno);
            _context.Update(turmaEncontrada);

            return new Aluno(
                novoAluno.Matricula,
                new Nome(novoAluno.PrimeiroNome, novoAluno.Sobrenome),
                new Email(novoAluno.Email));
        }

        public async Task RemoverAluno(int matricula)
        {
            var aluno = await _context.Alunos.FindAsync(matricula);

            _context.Alunos.Remove(aluno);
        }
    }
}

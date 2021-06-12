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
    public class TurmaRepository : ITurmaRepository
    {
        private readonly CursoIdiomasContext _context;

        public TurmaRepository(CursoIdiomasContext context)
        {
            _context = context;
        }

        public async Task<Turma> AlterarTurma(Turma turma)
        {
            var turmaAtualizada = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .ThenInclude(at => at.Aluno)
                                        .Where(t => t.Numero == turma.Numero)
                                        .FirstOrDefaultAsync();
            var idiomaAtualizado = await _context.Idiomas.Where(i => 
                                            i.Nome == turma.Idioma.Nome)
                                            .FirstOrDefaultAsync();
            
            if(idiomaAtualizado == null)
            {
                var novoIdioma = new IdiomaModel{ Nome = turma.Idioma.Nome };
                _context.Idiomas.Add(novoIdioma);
                idiomaAtualizado = novoIdioma;
            }

            turmaAtualizada.Idioma = idiomaAtualizado;
            _context.Turmas.Update(turmaAtualizada);

            return turma;
        }

        public async Task<List<Turma>> BuscarTodasAsTurmas()
        {
            var turmasEncontradas = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .ThenInclude(at => at.Aluno)
                                        .ToListAsync();

            var turmas = new List<Turma>();

            turmasEncontradas.ForEach(turma =>
            {
                var alunos = new List<Aluno>();

                turma.Alunos.ForEach(aluno =>
                {
                    alunos.Add(
                        new Aluno(aluno.Aluno.Matricula,
                                  new Nome(aluno.Aluno.PrimeiroNome, aluno.Aluno.Sobrenome),
                                  new Email(aluno.Aluno.Email)));
                });

                turmas.Add(
                    new Turma(turma.Numero,
                              new Idioma(turma.Idioma.Nome),
                              alunos));

            });

            return turmas;
        }

        public async Task<Turma> BuscarTurma(int Numero)
        {
            var turmaEncontrada = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .ThenInclude(at => at.Aluno)
                                        .Where(t => t.Numero == Numero)
                                        .FirstOrDefaultAsync();

            if (turmaEncontrada == null) return null;

            var alunos = new List<Aluno>();

            turmaEncontrada.Alunos.ForEach(aluno =>
            {
                alunos.Add(
                    new Aluno(aluno.Aluno.Matricula,
                              new Nome(aluno.Aluno.PrimeiroNome, aluno.Aluno.Sobrenome),
                              new Email(aluno.Aluno.Email)));
            });

            return new Turma(
                turmaEncontrada.Numero,
                new Idioma(turmaEncontrada.Idioma.Nome),
                alunos);
        }

        public async Task<List<Turma>> BuscarTurmasDeAluno(int matricula)
        {
            var turmasEncontradas = await _context.Turmas
                                        .Include(t => t.Idioma)
                                        .Include(t => t.Alunos)
                                        .ThenInclude(at => at.Aluno)
                                        .Where(t =>
                                            t.Alunos.Any(a => 
                                                a.Matricula == matricula)).ToListAsync();

            var turmas = new List<Turma>();

            turmasEncontradas.ForEach(turma =>
            {
                var alunos = new List<Aluno>();

                turma.Alunos.ForEach(aluno =>
                {
                    alunos.Add(
                        new Aluno(
                            aluno.Aluno.Matricula,
                            new Nome(aluno.Aluno.PrimeiroNome, aluno.Aluno.Sobrenome),
                            new Email(aluno.Aluno.Email)));
                });

                turmas.Add(
                    new Turma(
                        turma.Numero,
                        new Idioma(turma.Idioma.Nome),
                        alunos));

            });

            return turmas;
        }

        public async Task<Turma> CriarTurma(Turma turma)
        {
            var idioma = await _context.Idiomas.Where(i => i.Nome == turma.Idioma.Nome).FirstOrDefaultAsync();

            if(idioma == null)
                idioma = new IdiomaModel{ Nome = turma.Idioma.Nome };

            var novaTurma = new TurmaModel
            {
                Idioma = idioma,
                Alunos = new List<AlunoTurmaModel>()
            };

            _context.Turmas.Add(novaTurma);
            await _context.SaveChangesAsync();

            return new Turma(
                novaTurma.Numero,
                turma.Idioma,
                turma.Alunos);
        }

        public async Task RemoverTurma(int numero)
        {
            var turma = await _context.Turmas.FindAsync(numero);

            _context.Turmas.Remove(turma);
        }
    }
}

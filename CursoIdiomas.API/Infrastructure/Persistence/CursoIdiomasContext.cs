using CursoIdiomas.API.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Persistence
{
    public class CursoIdiomasContext : DbContext
    {
        public CursoIdiomasContext(DbContextOptions<CursoIdiomasContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlunoTurmaModel>()
                .HasKey(t => new { t.Matricula, t.Numero });

            modelBuilder.Entity<AlunoTurmaModel>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.Turmas)
                .HasForeignKey(at => at.Matricula);

            modelBuilder.Entity<AlunoTurmaModel>()
                .HasOne(at => at.Turma)
                .WithMany(t => t.Alunos)
                .HasForeignKey(at => at.Numero);
        }

        public DbSet<AlunoModel> Alunos { get; set; }
        public DbSet<TurmaModel> Turmas { get; set; }
        public DbSet<IdiomaModel> Idiomas { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<AlunoTurmaModel> AlunosTurmas { get; set; }

    }
}

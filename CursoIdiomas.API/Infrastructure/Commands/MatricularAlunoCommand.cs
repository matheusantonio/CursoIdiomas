﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class MatricularAlunoCommand : ICommand
    {
        public int MatriculaAluno { get; set; }
        public int NumeroTurma { get; set; }
    }
}

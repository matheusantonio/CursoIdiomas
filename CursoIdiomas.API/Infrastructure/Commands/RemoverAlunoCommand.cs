﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class RemoverAlunoCommand : ICommand
    {
        public int Matricula { get; set; }
    }
}

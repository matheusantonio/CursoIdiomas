﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Commands
{
    public class AlterarTurmaCommand : ICommand
    {
        public int Numero { get; set; }
        public string Idioma { get; set; }
    }
}

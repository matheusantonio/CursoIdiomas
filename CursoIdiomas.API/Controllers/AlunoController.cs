using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CursoIdiomas.API.Infrastructure.Repositories;
using CursoIdiomas.API.Infrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using CursoIdiomas.API.Infrastructure.Handlers;
using CursoIdiomas.API.Infrastructure.Commands.Results;
using CursoIdiomas.API.Infrastructure.UnitOfWork;
using CursoIdiomas.APIs.Infrastructure.Models.ReadModels;
using CursoIdiomas.API.Infrastructure.Queries;

namespace CursoIdiomas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoQueries _queries;
        private readonly IAlunoHandler _handler;

        public AlunoController(IAlunoQueries queries, IAlunoHandler handler)
        {
            _queries = queries;
            _handler = handler;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _queries.BuscarTodosAlunos());
        }

        [HttpGet("{matricula}")]
        public async Task<ActionResult> Get(int matricula)
        {
            var aluno = await _queries.BuscarAluno(matricula);
            
            if(aluno == null)
            {
                return NotFound();
            }
            
            return Ok(aluno);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CriarAlunoCommand matricularAlunoCommand)
        {
            var result = await _handler.Handle(matricularAlunoCommand);
            if(result.Success)
            {
                return Created("api/Aluno", result);
            }
            return Conflict(result);
        }

        [HttpPut("Matricula")]
        public async Task<ActionResult> Edit([FromBody] MatricularAlunoCommand matricularAlunoCommand)
        {
            return Ok(await _handler.Handle(matricularAlunoCommand));
        }

        [HttpDelete("Matricula")]
        public async Task<ActionResult> Delete([FromBody] DesmatricularAlunoCommand desmatricularAlunoCommand)
        {
            return Ok(await _handler.Handle(desmatricularAlunoCommand));
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody]AlterarAlunoCommand alterarAlunoCommand)
        {
            return Ok(await _handler.Handle(alterarAlunoCommand));
        }

        [HttpDelete("{matricula}")]
        public async Task<ActionResult> Delete(int matricula)
        {
            return Ok(await _handler.Handle(new RemoverAlunoCommand { Matricula = matricula }));
        }
    }
}

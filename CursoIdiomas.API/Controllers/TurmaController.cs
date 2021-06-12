using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Commands.Results;
using CursoIdiomas.API.Infrastructure.Handlers;
using CursoIdiomas.API.Infrastructure.Queries;
using CursoIdiomas.API.Infrastructure.Repositories;
using CursoIdiomas.API.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TurmaController : ControllerBase
    {
        private readonly ITurmaQueries _queries;
        private readonly ITurmaHandler _handler;

        public TurmaController(ITurmaQueries queries, ITurmaHandler handler)
        {
            _queries = queries;
            _handler = handler;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _queries.BuscarTodasTurmas());
        }

        [HttpGet("{numero}")]
        public async Task<ActionResult> Get(int numero)
        {
            var result = await _queries.BuscarTurma(numero);

            if(result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CriarTurmaCommand criarTurmaCommand)
        {
            var result = await _handler.Handle(criarTurmaCommand);
            if(result.Success)
            {
                return Created("api/Turma", result);
            }
            return Conflict(result);
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody]AlterarTurmaCommand alterarTurmaCommand)
        {
            return Ok(await _handler.Handle(alterarTurmaCommand));
        }

        [HttpDelete("{numero}")]
        public async Task<ActionResult> Delete(int numero)
        {
            return Ok(await _handler.Handle(new RemoverTurmaCommand { Numero = numero}));
        }

    }
}

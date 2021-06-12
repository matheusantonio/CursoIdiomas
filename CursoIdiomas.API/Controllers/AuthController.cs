using CursoIdiomas.API.Infrastructure.Authentication;
using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Handlers;
using CursoIdiomas.API.Infrastructure.Repositories;
using CursoIdiomas.API.Infrastructure.Persistence.Models;
using CursoIdiomas.API.Models.ApiInputModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CursoIdiomas.API.Infrastructure.UnitOfWork;

namespace CursoIdiomas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly ILoginHandler _handler;

        public AuthController(IUnitOfWork unitOfWork,
                               ITokenService tokenService,
                               ILoginHandler handler)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _handler = handler;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginInputModel inputModel)
        {
            var _usuario = await _unitOfWork.UsuarioRepository.VerificarUsuario(inputModel.Usuario, inputModel.Senha);

            if (_usuario == null)
                return NotFound(
                    new { 
                        success = false,
                        message = "Usuário ou senha inválidos" 
                    });

            var token = _tokenService.GerarToken(_usuario);

            return Ok(
                new {
                    usuario = _usuario,
                    token = token
                });
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody]RegistrarUsuarioCommand registrarUsuarioCommand)
        {
            return Ok(await _handler.Handle(registrarUsuarioCommand));
        }
    }
}

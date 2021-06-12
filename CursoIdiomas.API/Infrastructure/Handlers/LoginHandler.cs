using CursoIdiomas.API.Infrastructure.Commands;
using CursoIdiomas.API.Infrastructure.Commands.Results;
using CursoIdiomas.API.Infrastructure.Repositories;
using CursoIdiomas.API.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CursoIdiomas.API.Infrastructure.Handlers
{
    public class LoginHandler : ILoginHandler
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ICommandResult> Handle(RegistrarUsuarioCommand command)
        {
            var usuarioExistente = await _unitOfWork.UsuarioRepository.RecuperarUsuario(command.Usuario);
            if (usuarioExistente != null)
                return new CommandResult(false, "Nome de usuário já existe");

            _unitOfWork.UsuarioRepository.CriarUsuario(command.Usuario, command.Senha);
            await _unitOfWork.Salvar();

            return new CommandResult(true, "Usuário registrado com sucesso");
        }
    }
}

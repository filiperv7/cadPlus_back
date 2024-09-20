using AutoMapper;
using CadPlus.API.Models;
using CadPlus.Application.Helpers;
using CadPlus.Application.Services;
using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CadPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserService _createUserService;
        private readonly ILoginService _loginService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(ICreateUserService createUserService, ILoginService loginService, ITokenService tokenService, IMapper mapper)
        {
            _loginService = loginService;
            _createUserService = createUserService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserCreationDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _mapper.Map<User>(userDto);

            bool created = await _createUserService.CreateUser(user, userDto.IdProfile);

            if (created) return Ok(new { Message = "Usuário criado com sucesso!" });

            return BadRequest(new {Message = "Email ou CPF já utilizado por outro usuário!"});
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            string token = await _loginService.Login(loginDto.Email, loginDto.Password);

            if (token == null) return Unauthorized(new { Message = "Email ou senha inválidos." });

            return Ok(new { Token = $"Bearer {token}" });
        }
    }
}

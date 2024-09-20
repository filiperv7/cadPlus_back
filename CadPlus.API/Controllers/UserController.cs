using AutoMapper;
using CadPlus.API.Models;
using CadPlus.Domain.Entities;
using CadPlus.Domain.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadPlus.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ICreateUserService _createUserService;
        private readonly ILoginService _loginService;
        private readonly IFindUsersServices _findUsersServices;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserController(ICreateUserService createUserService, ILoginService loginService, IFindUsersServices findUsersServices, ITokenService tokenService, IMapper mapper)
        {
            _loginService = loginService;
            _createUserService = createUserService;
            _findUsersServices = findUsersServices;
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

            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            try
            {
                bool created = await _createUserService.CreateUser(user, userDto.IdProfile, token);

                if (created) return Ok(new { Message = "Usuário criado com sucesso!" });
                else return BadRequest(new { Message = "Email ou CPF já utilizado por outro usuário!" });
            }
            catch (UnauthorizedAccessException error)
            {
                return Forbid(error.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            string token = await _loginService.Login(loginDto.Email, loginDto.Password);

            if (token == null) return Unauthorized(new { Message = "Email ou senha inválidos." });

            return Ok(new { Token = $"Bearer {token}" });
        }

        [Authorize]
        [HttpGet]
        [Route("profile/{idProfile}")]
        public async Task<IActionResult> FindUsersByProfile(int idProfile)
        {
            var users = await _findUsersServices.FindUsersByProfile(idProfile);

            if (!users.Any())
            {
                return NotFound(new { Message = "Nenhum usuário encontrado com o perfil especificado." });
            }

            var usersDto = _mapper.Map<IEnumerable<UserResponseDto>>(users);
            return Ok(usersDto);
        }
    }
}

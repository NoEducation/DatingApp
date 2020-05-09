using AutoMapper;
using Dating.Common.Configurations;
using Dating.DTO;
using Dating.Infrastrucutre;
using Dating.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dating.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<UserController> _logger;
        private readonly TokenConfiguration _configuraiton;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepository, ILogger<UserController> logger,
            TokenConfiguration configuraiton,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _logger = logger;
            _configuraiton = configuraiton;
            _mapper = mapper;
        }

        [Route("Register")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserLogin user)
        {
            if (await _authRepository.UserExist(user.UserName))
                return BadRequest();


            var username = user.UserName.ToLower();

            try
            {
                await this._authRepository.Register(new UserModel() { Name = user.UserName }, user.Password);
            }
            catch (Exception ex)
            {
                this._logger.LogError("Error occured when creating new user , error details {0}", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            if (!await this._authRepository.UserExist(user.UserName))
                return Unauthorized(401);

            var userFromRepo = await _authRepository.Login(user.UserName.ToLower(), user.Password);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuraiton.Token));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var userToReturn = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                user = userToReturn
            });
        }
    }
}

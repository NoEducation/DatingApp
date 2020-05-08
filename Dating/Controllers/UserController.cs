using AutoMapper;
using Dating.DTO;
using Dating.Infrastrucutre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dating.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDatingRepository _datingRepository;
        private readonly IUsersPdfGenerator _userPdfGenerator;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(
            IDatingRepository datingRepository,
            IUsersPdfGenerator userPdfGenerator,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            this._datingRepository = datingRepository;
            this._userPdfGenerator = userPdfGenerator;
            this._logger = logger;
            this._mapper = mapper;
        }


        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _datingRepository.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(result);

            return Ok(usersToReturn);
        } 

        [HttpGet("UserById/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var result = await _datingRepository.GetUserById(userId);
            if (result == null)
                return NotFound();

            var userToReturn = _mapper.Map<UserForDetailedDto>(result);

            return Ok(userToReturn);
        }

        [HttpPost("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UserForUdpateDto user ) 
        {   

            if (userId != int.Parse( User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await this._datingRepository.GetUserById(userId);

            _mapper.Map(user, userFromRepo);

            if (await _datingRepository.SaveAll())
                return NoContent();

            throw new System.Exception("Something bad happaned inside User controller");
        }

        [HttpGet("DownloadAsPdf")]
        public async Task<IActionResult> DownloadAsPdf()
        {
            var file = await _userPdfGenerator.GeneratePfdFile();
            return File(file.FileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", file.Name);

        }

    }
}

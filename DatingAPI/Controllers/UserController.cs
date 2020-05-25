using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DatingAPI.Common.Extensions;
using DatingAPI.DTO;
using DatingAPI.Infrastrucutre;
using DatingCommon.CQRS.Interfaces;
using DatingLogic.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DatingAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IDispacher _dispacher;
        private readonly IDatingRepository _datingRepository;
        private readonly IUsersPdfGenerator _userPdfGenerator;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;

        public UserController(
            IDatingRepository datingRepository,
            IUsersPdfGenerator userPdfGenerator,
            ILogger<UserController> logger,
            IMapper mapper,
            IDispacher dispacher)
        {
            this._datingRepository = datingRepository;
            this._userPdfGenerator = userPdfGenerator;
            this._logger = logger;
            this._mapper = mapper;
            this._dispacher = dispacher;
        }


        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber , [FromQuery] int pageSize)
        {
            var result = await _datingRepository.GetUsers(new UserParams(){PageNumber =  pageNumber, PageSize = pageSize});
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(result);

            await this._dispacher.HandleAsync(new BaseCommand());

            this.Response.AddPagination(result.CurrentPage, result.PageSize, result.TotalItems, result.TotalPages);

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

using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Dating.Common.Configurations;
using Dating.DTO;
using Dating.Infrastrucutre;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Dating.Models;

namespace Dating.Controllers
{
    [Route("api/user/{userId}/photos")]
    [ApiController]
    [Authorize]
    public class PhotoController : ControllerBase
    {
        private readonly IDatingRepository _datingRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloundinarySettings> _cloudinarySettings;
        private readonly Cloudinary _cloudinary;

        public PhotoController(IDatingRepository datingRepository,IMapper mapper,
            IOptions<CloundinarySettings> cloudinarySettings)
        {
            this._datingRepository = datingRepository;
            this._mapper = mapper;
            this._cloudinarySettings = cloudinarySettings;

            var account = new Account(
                this._cloudinarySettings.Value.CloudName,
                this._cloudinarySettings.Value.ApiKey,
                this._cloudinarySettings.Value.ApiSecret);

            _cloudinary = new Cloudinary(account);
        }


        [HttpGet("id", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = _datingRepository.GetPhoto(id);
            var photoDto = _mapper.Map<PhotoForReturnDto>(photoFromRepo);
            return Ok(photoDto);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId,[FromForm] PhotoForCreatingDto photoForCreatingDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await this._datingRepository.GetUserById(userId);

            var file = photoForCreatingDto.File;

            var uploadResult = new ImageUploadResult();

            if(file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uploadResult = await _cloudinary.UploadAsync(uploadParams);
                 }
            }

            photoForCreatingDto.Url = uploadResult.Uri.ToString();
            photoForCreatingDto.PublicId = uploadResult.PublicId.ToString();

            var photo = _mapper.Map<PhotoModel>(photoForCreatingDto);

            if (!userFromRepo.Photos.Any(u => u.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);

            if(await _datingRepository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo");
        }
        [HttpPost("{id}/isMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _datingRepository.GetUserById(userId);

            if (!user.Photos.Any(x => x.Id == id))
                return Unauthorized();

            var photoFromRepo = await _datingRepository.GetPhoto(id);

            if (photoFromRepo.IsMain)
                return BadRequest("This photo already is set as main");

            var currentMainPhoto = await _datingRepository.GetMainPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            photoFromRepo.IsMain = true;

            if (await _datingRepository.SaveAll())
                return NoContent();

            return BadRequest("Could not set photo is main");
        }
    }
}
﻿using System;
using Microsoft.AspNetCore.Http;

namespace DatingAPI.DTO
{
    public class PhotoForCreatingDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        public PhotoForCreatingDto()
        {
            this.DateAdded = DateTime.Now;
        }
    }
}

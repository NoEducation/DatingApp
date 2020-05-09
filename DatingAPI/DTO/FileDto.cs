using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.DTO
{
    public class FileDto
    {
        public  string Name { get; set; }
        public byte[] FileBytes { get; set; }
        public FileDto(string name , byte[] fileBytes)
        {
            Name = name;
            FileBytes = fileBytes;
        }
    }
}

namespace DatingAPI.DTO
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

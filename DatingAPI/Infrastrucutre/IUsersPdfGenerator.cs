using Dating.DTO;
using System.Threading.Tasks;

namespace Dating.Infrastrucutre
{
    public interface IUsersPdfGenerator
    {
        Task<FileDto> GeneratePfdFile();
    }
}
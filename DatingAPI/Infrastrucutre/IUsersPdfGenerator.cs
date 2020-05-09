using System.Threading.Tasks;
using DatingAPI.DTO;

namespace DatingAPI.Infrastrucutre
{
    public interface IUsersPdfGenerator
    {
        Task<FileDto> GeneratePfdFile();
    }
}
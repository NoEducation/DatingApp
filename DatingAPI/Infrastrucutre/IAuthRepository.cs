using System.Threading.Tasks;
using DatingAPI.Models;

namespace DatingAPI.Infrastrucutre
{
    public interface IAuthRepository
    {
        Task<UserModel> Register(UserModel user, string password);
        Task<UserModel> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}

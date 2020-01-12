using Dating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Infrastrucutre
{
    public interface IAuthRepository
    {
        Task<UserModel> Register(UserModel user, string password);
        Task<UserModel> Login(string username, string password);
        Task<bool> UserExist(string username);
    }
}

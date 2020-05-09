using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dating.DAT;
using Dating.Models;
using Microsoft.EntityFrameworkCore;

namespace Dating.Infrastrucutre
{
    public class AuthDefaultRepository : IAuthRepository
    {
        private readonly DatingContext _contex;
        public AuthDefaultRepository(DatingContext contex)
        {
            this._contex = contex;
        }

        public async Task<UserModel> Login(string username, string password)
        {
            var user = await _contex.Users.Include(x => x.Photos)
                .SingleOrDefaultAsync(x => x.Name == username);

            if (user is null)
                return null;

            if (!CheckPasswordIsCorrect(username, password, user.PasswordSalt,user.PasswordHash))
                return null;

            return user;
        }

        public async Task<UserModel> Register(UserModel user, string password)
        {
            (var hashedPassowrd, var passowrdSalt) =  GeneratePassowrdHashWithSalt(password);
            user.PasswordHash = hashedPassowrd;
            user.PasswordSalt = passowrdSalt;

            await _contex.Users.AddAsync(user);
            await _contex.SaveChangesAsync();

            return user;
        }
        public async Task<bool> UserExist(string username)
       => await _contex.Users.AnyAsync(x => x.Name == username);

        private bool CheckPasswordIsCorrect(string username, string password, byte[] passwordSalt, byte[] userPassoword)
        {
            using (var hash = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != userPassoword[i])
                        return false;
                }
            }
            return true;
        }
        private (byte[],byte[]) GeneratePassowrdHashWithSalt(string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using(var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return (passwordHash, passwordSalt);
        }

    }
}

using Dating.DTO;
using Dating.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.DAT
{
    public static class Seed
    {   

        /// <summary>
        /// Ta metoda bedzie wywoływana na początku startu projektu
        /// zatem nie ma potrzeby tworzenia jej asynchronicznej
        /// Najlepszym miejscem użycia tej klasy według microsoftu jest program start
        /// </summary>
        /// <param name="context"></param>
        public static void SeedUsers(DatingContext context)
        {
            if (!context.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("DAT/UserEntitySeed.json");
                var users = JsonConvert.DeserializeObject<List<UserModel>>(userData);
                foreach (var user in users)
                {
                    (byte[] passwordHash, byte[] passwordSalt) = GeneratePassowrdHashWithSalt("password");
                    user.PasswordHash = passwordHash;
                    user.PasswordSalt = passwordSalt;
                    user.Name = user.Name.ToLower();
                    context.Add<UserModel>(user);
                }
                context.SaveChanges();
            }
        }
        private static (byte[], byte[]) GeneratePassowrdHashWithSalt(string password)
        {
            byte[] passwordHash;
            byte[] passwordSalt;
            using (var hash = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            return (passwordHash, passwordSalt);
        }
    }
}

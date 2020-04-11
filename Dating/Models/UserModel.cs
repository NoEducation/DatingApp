using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dating.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string KnowAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoModel> Photos { get; set; }

        #region Constructors

        public UserModel()
        {
                
        }

        public UserModel(int userId, string name, byte[] passwordHash, byte[] passwordSalt, string gender, DateTime dateOfBirth, string knowAs, DateTime created, DateTime lastActive, string introduction, string lookingFor, string interests, string city, string country, ICollection<PhotoModel> photos)
        {
            UserId = userId;
            Name = name;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            Gender = gender;
            DateOfBirth = dateOfBirth;
            KnowAs = knowAs;
            Created = created;
            LastActive = lastActive;
            Introduction = introduction;
            LookingFor = lookingFor;
            Interests = interests;
            City = city;
            Country = country;
            Photos = photos;
        }
        #endregion
       
    }
}

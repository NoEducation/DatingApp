using System;

namespace Dating.DTO
{
    public class UserLogin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string KnowAs { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public DateTime DateOfBirth { get; set; }

        public UserLogin()
        {
            this.Created = DateTime.Now;
            this.LastActive = DateTime.Now;
        }
    }
}

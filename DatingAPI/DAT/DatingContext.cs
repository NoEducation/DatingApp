using Dating.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Dating.DAT
{
    public class DatingContext : DbContext
    {
        public DatingContext(DbContextOptions<DatingContext> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PhotoModel> Photos { get; set; }
    }
}

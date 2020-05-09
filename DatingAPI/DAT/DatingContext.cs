using DatingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingAPI.DAT
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

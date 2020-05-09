using System.Linq;
using System.Threading.Tasks;
using DatingAPI.Common.Models;
using DatingAPI.DAT;
using DatingAPI.DTO;
using DatingAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingAPI.Infrastrucutre
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DatingContext _context;
        public DatingRepository(DatingContext context)
        {
            this._context = context;
        }

        public async Task Add<T>(T enity) where T : class
        {
            await _context.AddAsync<T>(enity);
        }

        public async Task Delete<T>(T entity) where T : class
        {
            await _context.AddAsync<T>(entity);
        }

        public async Task<PhotoModel> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(x => x.IsMain);
        }

        public async Task<PhotoModel> GetPhoto(int photoId)
        {
            return await _context.Photos.FirstOrDefaultAsync(x => x.Id == photoId);
        }

        public async Task<UserModel> GetUserById(int UserId)
        {
            return await _context.Users
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.UserId == UserId);

        }

        public async Task<PageList<UserModel>> GetUsers(UserParams userParams)
        {
            var items =  _context.Users.Include(x => x.Photos).AsQueryable();
            return await PageList<UserModel>.CreateAsync(items, userParams.PageNumber, userParams.PageSize);

        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

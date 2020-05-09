using Dating.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dating.Common.Models;
using Dating.DTO;

namespace Dating.Infrastrucutre
{
    public interface IDatingRepository
    {
        Task Add<T>(T enity) where T : class;
        Task Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PageList<UserModel>> GetUsers(UserParams userParams);
        Task<UserModel> GetUserById(int UserId);
        Task<PhotoModel> GetPhoto(int photoId);
        Task<PhotoModel> GetMainPhotoForUser(int userId);
    }
}

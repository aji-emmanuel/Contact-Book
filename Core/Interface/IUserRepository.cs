using System.Collections.Generic;
using UserManagement.DTOs;
using UserManagement.Model;
using System.Threading.Tasks;
using UserManagement.Hateoas;

namespace UserManagement.Core
{
    public interface IUserRepository
    {
        PagedList<User> GetAllUsers(int pageNo);
        Task<User> AddUser(User user);
        Task<bool> DeleteUser(string userId);
        Task<User> GetUserById(string userId);
        Task<User> GetUserByEmail(string email);
        IEnumerable<User> SearchUsers(UserActionParams userActionParams);
        Task<bool> UpdateUser(UpdateRequestDTO updateUser, string id);
        Task<bool> UpdateAvatarUrl(string Url, string Id);
    }
}
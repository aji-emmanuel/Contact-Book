using System.Threading.Tasks;
using UserManagement.DTOs;
using UserManagement.Model;

namespace UserManagement.Core
{
    public interface IAuthUser
    {
        Task<User> Login(LoginRequestDTO userRequest);
        Task<User> Register(User user);
    }
}
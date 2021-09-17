using System.Threading.Tasks;
using UserManagement.Model;

namespace UserManagement.Core
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}
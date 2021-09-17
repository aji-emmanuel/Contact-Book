using System;
using System.Threading.Tasks;
using UserManagement.DTOs;
using UserManagement.Model;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Core
{
    public class AuthUser : IAuthUser
    {
        private readonly UserManager<User> _userManager;
      
        public AuthUser(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> Login(LoginRequestDTO userRequest)
        {
            User user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password) == true)
                {
                    return user;
                }
                throw new AccessViolationException("Wrong UserName or Password");
            }
            throw new AccessViolationException("Wrong UserName or Password");
        }

        public async Task<User> Register(User user)
        {
            user.UserName = String.IsNullOrWhiteSpace(user.UserName) ? user.Email : user.UserName;
            user.CreatedAt = DateTime.Now;

            IdentityResult result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                return user;
            }
            string errors = String.Empty;
            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }
            throw new MissingFieldException(errors);
        }
    }
}

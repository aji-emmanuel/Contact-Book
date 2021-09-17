using System;
using AutoMapper;
using UserManagement.Core;
using UserManagement.DTOs;
using UserManagement.Model;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthUser _authUser;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;

        public AuthController(IAuthUser authUser, ITokenGenerator tokenGenerator, IMapper mapper)
        {
            _authUser = authUser;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
        }

        //Handles users login requests
        [HttpPost]
        [Route("login", Name = "Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            try
            {
                User user = await _authUser.Login(loginRequest);
                var loggedUser = _mapper.Map<LoginResponseDTO>(user);
                loggedUser.Token = await _tokenGenerator.GenerateToken(user);
                return Ok(loggedUser);
            }
            catch (AccessViolationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //Handles users registration request
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegRequestDTO regRequest)
        {
            try
            {
                User user = _mapper.Map<User>(regRequest);
                var result = await _authUser.Register(user);
                return Ok("Registration Successful");
            }
            catch (MissingFieldException errors)
            {
                return BadRequest(errors.Message);
            }
        }
    }
}

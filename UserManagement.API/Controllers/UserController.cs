using AutoMapper;
using ImageUploadService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Core;
using UserManagement.DTOs;
using UserManagement.Hateoas;
using UserManagement.Model;

namespace UserManagement.API.Controllers
{
    [ApiController]
    [Route("api/User")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IImageService imageService, IMapper mapper)
        {
            _userRepository = userRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new user to the datastore
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("add-new")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddNewUser([FromBody] RegRequestDTO newUser)
        {
            try
            {
                User user = _mapper.Map<User>(newUser);
                var result = await _userRepository.AddUser(user);
                return Ok("User Added Successfully");
            }
            catch (MissingFieldException errors)
            {
                return BadRequest(errors.Message);
            }
        }

        /// <summary>
        /// Returns a paginated list of all users
        /// </summary>
        /// <param name="userActionParams"></param>
        /// <returns></returns>
        [HttpGet("all-users", Name = "GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAllUsers(int page)
        {
            if (page <= 0)
                page = 1;
            PagedList<User> listOfUsers = _userRepository.GetAllUsers(page);

            var previousPageLink = listOfUsers.HasPrevious ?
                PageUrl(page, PageLinkType.PreviousPage) : null;

            var nextPageLink = listOfUsers.HasNext ?
                PageUrl(page, PageLinkType.NextPage) : null;

            var paginationMetaData = new
            {
                totalCount = listOfUsers.TotalCount,
                pageSize = listOfUsers.PageSize,
                currentPage = listOfUsers.CurrentPage,
                totalPages = listOfUsers.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetaData));
            return Ok(_mapper.Map<IEnumerable<GetUserDTO>>(listOfUsers));
        }

        /// <summary>
        /// Fetches Users using a search word or filtering by state name
        /// </summary>
        /// <param name="userActionParams"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public IActionResult SearchUsers([FromQuery] UserActionParams userActionParams)
        {
            IEnumerable<User> listOfUsers = _userRepository.SearchUsers(userActionParams);
            if (listOfUsers.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<GetUserDTO>>(listOfUsers));
        }

        /// <summary>
        /// Returns a user by using user Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(string id)
        {
            User user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUserDTO>(user));
        }

        /// <summary>
        /// Returns a user by using user email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("email")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            User user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetUserDTO>(user));
        }

        /// <summary>
        /// Updates the records of an existing user
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UpdateRequestDTO updateUser)
        {
            try
            {
                string id = HttpContext.User.FindFirst(text => text.Type == ClaimTypes.NameIdentifier).Value;
                bool result = await _userRepository.UpdateUser(updateUser, id);
                if (result == true)
                    return NoContent();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates the Avatar Url of a user
        /// </summary>
        /// <param name="imageDto"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("photo")]
        public async Task<IActionResult> UploadProfilePic([FromForm] ImageDto imageDto)
        {
            string id = HttpContext.User.FindFirst(text => text.Type == ClaimTypes.NameIdentifier).Value;
            try
            {
                var upload = _imageService.ImageUploadAsync(imageDto.Image);
                string url = upload.Result.Url.ToString();

                bool result = await _userRepository.UpdateAvatarUrl(url, id);
                if (result == true)
                    return NoContent();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Removes a user from the data store using the user Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserById(string id)
        {
            try
            {
                bool result = await _userRepository.DeleteUser(id);
                if (result == true)
                    return NoContent();
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Creates previous and next page urls for the response header
        /// </summary>
        /// <param name="page"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string PageUrl(int page, PageLinkType type)
        {
            return type switch
            {
                PageLinkType.PreviousPage => Url.Link("GetAllUsers",
                new
                {
                    PageNumber = page - 1,
                }),
                PageLinkType.NextPage => Url.Link("GetAllUsers",
                new
                {
                    PageNumber = page + 1,
                }),
                _ => Url.Link("GetAllUsers",
                new
                {
                    page,
                }),
            };
        }
    }
}

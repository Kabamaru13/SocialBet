using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using SocialBet.Helpers;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using SocialBet.Services;
using SocialBet.Dtos;
using SocialBet.Models;

namespace SocialBet.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(IUserService userService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDto userDto)
        {
            var user = _userService.Authenticate(userDto.Username, userDto.Password);

            if (user == null)
                return BadRequest(new ResultData() { data = null, error = "Username or password is incorrect" });
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

                var tokenString = tokenHandler.WriteToken(token);

                // return basic user info (without password) and token to store client side
                return Ok(new ResultData()
                {
                    data = new
                    {
                        Id = user.Id,
                        Username = user.Username,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Token = tokenString
                    },
                    error = null
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]UserDto userDto)
        {
            // map dto to entity
            var user = _mapper.Map<User>(userDto);

            try
            {
                // save 
                var newUser = _userService.Create(user, userDto.Password);
                return Ok(new ResultData() { data = newUser, error = null });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var users = _userService.GetAll();
                var userDtos = _mapper.Map<IList<UserDto>>(users);
                return Ok(new ResultData() { data = userDtos, error = null });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var user = _userService.GetById(id);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(new ResultData() { data = userDto, error = null });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserDto userDto)
        {
            // map dto to entity and set id
            var user = _mapper.Map<User>(userDto);
            user.Id = id;

            try
            {
                // save 
                var updated = _userService.Update(user, userDto.Password);
                return Ok(new ResultData() { data = updated, error = null });
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(new ResultData() { data = "User deleted successfully", error = null });
            }
            catch(Exception ex)
            {
                return BadRequest(new ResultData() { data = null, error = ex.Message });
            }
        }
    }
}

using JVT_React_Core.Data;
using JVT_React_Core.Data.Models;
using JVT_React_Core.DTO;
using JVT_React_Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JVT_React_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDtos dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            };
            return Created("Success", _userRepository.Create(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDtos dto)
        {
            var userr = _userRepository.Login(dto);
            var jwt = _jwtService.Generete(userr.Id);
            Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });
            if (userr is not null)
            {
                return Ok(userr);
            }
            return BadRequest("404");
        }

        [HttpDelete("delete-user")]
        public IActionResult DeleteUser(DeleteDtos dto)
        {
            _userRepository.Delete(dto);
            return Ok();
        }

        [HttpPut("update-user")]
        public IActionResult UpdateUser(UpdateDtos dto)
        {
            var updatedBook = _userRepository.Update(dto);
            return Ok(updatedBook);
        }
    }
}

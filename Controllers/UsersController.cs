using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LfragmentApi.Data;
using LfragmentApi.Entities;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using LfragmentApi.DTOs;

namespace LfragmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = new User()
            {
                Created = DateTime.UtcNow,
                Email = registerUserDto.Email,
                UserName = registerUserDto.Username
            };
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (result.Succeeded)
            {
                return Ok("Registration succeded");
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
               userName: loginUserDto.Email,
               password: loginUserDto.Password,
               isPersistent: true,
               lockoutOnFailure: false
               );
            if (signInResult.Succeeded) 
            {
            return Ok();
            }
            return BadRequest(loginUserDto.Email);
        }
    }
}
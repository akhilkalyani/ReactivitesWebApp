using System.Security.Claims;
using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly TockenService _tockenService;
        public AccountController(UserManager<AppUser> userManager,TockenService tockenService)
        {
            _tockenService = tockenService;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user=await _userManager.FindByEmailAsync(loginDTO.Email);
            if(user==null)return Unauthorized();
            var result=await _userManager.CheckPasswordAsync(user,loginDTO.Password);
            if(result)
            {
                return CreateUserObject(user);
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if(await _userManager.Users.AnyAsync(x=>x.UserName==registerDTO.UserName))
            {
                ModelState.AddModelError("username","Username is already taken!");
                return ValidationProblem();
            }
            if(await _userManager.Users.AnyAsync(x=>x.Email==registerDTO.Email))
            {
                ModelState.AddModelError("email","Email is already taken!");
                return ValidationProblem();
            }
            var user=new AppUser
            {
                DisplayName=registerDTO.DisplayName,
                Email=registerDTO.Email,
                UserName=registerDTO.UserName
            };

            var result=await _userManager.CreateAsync(user,registerDTO.Password);

            if(result.Succeeded)
            {
                return CreateUserObject(user);
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var user=await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));
            return CreateUserObject(user);
        }
        private UserDTO CreateUserObject(AppUser user)
        {
            return new UserDTO
            {
                DisplayName = user.DisplayName,
                Image = null,
                Tocken = _tockenService.CreateTocken(user),
                UserName = user.UserName
            };
        }
    }
}
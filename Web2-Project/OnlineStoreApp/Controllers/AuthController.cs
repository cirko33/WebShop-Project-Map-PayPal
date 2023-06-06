using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreApp.DTOs;
using OnlineStoreApp.Exceptions;
using OnlineStoreApp.Interfaces.IServices;
using System.Security.Principal;

namespace OnlineStoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var token = await _authService.Login(loginDTO);
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            await _authService.Register(registerDTO);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("google-sign-in")]
        public async Task<IActionResult> GoogleSignIn(TokenDTO token)
        {
            var returnToken = await _authService.GoogleSignIn(token);
            return Ok(returnToken);
        }
    }
}

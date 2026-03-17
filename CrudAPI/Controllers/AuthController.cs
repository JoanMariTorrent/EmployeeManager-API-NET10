using Azure.Core;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using CrudAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;


        public AuthController(AuthService context)
        {
            _authService = context;
        }

        // Show all accounts
        [HttpGet("Get accounts")]
        [Authorize(Roles = StaticRoles.Admin)]
        public async Task<ActionResult> GetAccounts()
        {
            return Ok(await _authService.GetAccounts());
        }

        // Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDTO request)
        {
            var result = await _authService.Register(request);
            if(result == null) return BadRequest("Este nombre ya existe, prueba con otro");
            return Ok("Usuario añadido con exito");
        }


        // Sign in
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(RegisterDTO request)
        {
            var result = await _authService.LogIn(request);
            return Ok(new
            {
                result,
                message = "Token exitoso!"
            });
        }


        // Delete
        [HttpDelete("Delete Account")]
        [Authorize(Roles = StaticRoles.Admin)]
        public async Task<ActionResult> DeleteAccount(RegisterDTO request)
        {
            var result = await _authService.DeleteAccount(request);
            return Ok(result);
        }


        [HttpGet("me")]
        [Authorize]
        public async Task<ActionResult<UserSessionDTO>> GetMe()
        { 
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(userIdClaim == null) return Unauthorized();

            var userData = await _authService.GetCurrentUserData(int.Parse(userIdClaim));

            if(userData == null) return NotFound("Usuario no encontrado");

            return Ok(userData);
        }
    }
}

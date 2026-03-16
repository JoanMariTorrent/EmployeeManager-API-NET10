using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CrudAPI.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Show all accounts
        public async Task<List<UsuarioDTO>> GetAccounts()
        {
            var users = await _context.Usuarios
                .Select(u => new UsuarioDTO 
                    { Id = u.Id, 
                    Username = u.Username, 
                    Rol = u.Rol })
                .ToListAsync();

            return users;
        }

        // Register
        public async Task<Usuario> Register(RegisterDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var rol = StaticRoles.Employee;
            if (request.RolType == StaticRoles.Admin &&
                !string.IsNullOrEmpty(request.AdminPassword) &&
                request.AdminPassword == _configuration.GetSection("AdminSettings:SecretKey").Value!
                )
                rol = StaticRoles.Admin;

            if (await _context.Usuarios.AnyAsync(u => u.Username == request.Username))
                return null;

            var nuevoEmpleado = new Empleado
            {
                NombreCompleto = request.Username,
                Sueldo = 0,
                IdPerfil = 1
            };

            var usuario = new Usuario
            {
                Username = request.Username,
                PasswordHash = passwordHash,
                Rol = rol,
                Empleado = nuevoEmpleado
            };

            await _context.Empleados.AddAsync(nuevoEmpleado);
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        // Sign in
        public async Task<String?> LogIn(RegisterDTO request)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            return CreateToken(user);
        }

        // Delete
        public async Task<String> DeleteAccount(RegisterDTO request)
        {
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Username == request.Username);
            if (user == null) return "Usuario no encontrado";
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return "Contraseña incorrecta";

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return "Cuenta eliminada con exito!";
        }




        private string CreateToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

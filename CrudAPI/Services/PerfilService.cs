using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;


namespace CrudAPI.Services
{
    public class PerfilService
    {
        private readonly AppDbContext _context;
        public PerfilService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PerfilDTO>> GetPerfiles()
        {
            return await _context.Perfiles
                .Select(p => new PerfilDTO 
                {
                    IdPerfil = p.IdPerfil,
                    Nombre = p.Nombre
                })
                .ToListAsync();
        }

    }
}

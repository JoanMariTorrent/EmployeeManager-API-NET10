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
            var listaDTO = new List<PerfilDTO>();
            var listaDB = await _context.Perfiles.ToListAsync();


            foreach (var item in listaDB)
                listaDTO.Add(new PerfilDTO
                {
                    IdPerfil = item.IdPerfil,
                    Nombre = item.Nombre
                });

            return listaDTO;
        }

    }
}

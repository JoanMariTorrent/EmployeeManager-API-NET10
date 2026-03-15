using Microsoft.EntityFrameworkCore;
using CrudAPI.Context;
using CrudAPI.DTOs;
using CrudAPI.Entities;

namespace CrudAPI.Services
{
    public class EmpleadoService
    {
        private readonly AppDbContext _context;
        public EmpleadoService(AppDbContext context)
        {
            _context = context;
        }


        private EmpleadoDTO ToDTO(Empleado item) => new EmpleadoDTO
        {
            IdEmpleado = item.IdEmpleado,
            NombreCompleto = item.NombreCompleto,
            Sueldo = item.Sueldo,
            IdPerfil = item.IdPerfil,
            NombrePerfil = item.PerfilReferencia?.Nombre
        };


        //Get all Empleados
        public async Task<List<EmpleadoDTO>> GetAllEmpleados()
        {
            var listDB = await _context.Empleados.Include(p => p.PerfilReferencia).ToListAsync();
            return listDB.Select(e => ToDTO(e)).ToList();
        }

        // Search Empleado from ID
        public async Task<EmpleadoDTO> GetEmpleadoFromID(int id)
        {
            var empleadoDB = await _context.Empleados
                .Include(p => p.PerfilReferencia)
                .FirstOrDefaultAsync(e => e.IdEmpleado == id);

            return empleadoDB != null ? ToDTO(empleadoDB) : null;
        }

        // Add Empleado
        public async Task<Empleado> AddEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = new Empleado
            {
                NombreCompleto = empleadoDTO.NombreCompleto,
                Sueldo = empleadoDTO.Sueldo,
                IdPerfil = empleadoDTO.IdPerfil
            };

            await _context.Empleados.AddAsync(empleadoDB);
            await _context.SaveChangesAsync();
            return empleadoDB;
        }

        // Edit Empleado
        public async Task<String> EditEmpleado(EmpleadoDTO empleadoDTO)
        {
            var empleadoDB = await _context.Empleados.FindAsync(empleadoDTO.IdEmpleado);

            if (empleadoDB == null) return "Empleado no encontrado";

            empleadoDB.NombreCompleto = empleadoDTO.NombreCompleto;
            empleadoDB.Sueldo = empleadoDTO.Sueldo;
            empleadoDB.IdPerfil = empleadoDTO.IdPerfil;

            _context.Empleados.Update(empleadoDB);
            await _context.SaveChangesAsync();

            return ("Empleado modificado");
        }

        // Remove Empleado from ID
        public async Task<String> RemoveEmpleadoFromID(int id)
        {
            var empleadoDB = await _context.Empleados.FindAsync(id);

            if (empleadoDB == null)
            {
                return ("Empleado no encontrado");
            }

            _context.Empleados.Remove(empleadoDB);
            await _context.SaveChangesAsync();
            return ("Empleado Eliminado");
        }
    }
}

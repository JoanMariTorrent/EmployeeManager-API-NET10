using System.ComponentModel.DataAnnotations.Schema;

namespace CrudAPI.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? Rol {  get; set; }

        public int? IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public Empleado? Empleado { get; set; }
    }
}

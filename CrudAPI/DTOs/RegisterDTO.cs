namespace CrudAPI.DTOs
{
    public class RegisterDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string RolType  { get; set; }
        public required string AdminPassword { get; set; }
    }
}

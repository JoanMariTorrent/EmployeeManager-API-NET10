using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CrudAPI.Migrations
{
    /// <inheritdoc />
    public partial class RelacionFinalCorrecta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpleado",
                table: "Usuarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdEmpleado",
                table: "Usuarios",
                column: "IdEmpleado");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Empleados_IdEmpleado",
                table: "Usuarios",
                column: "IdEmpleado",
                principalTable: "Empleados",
                principalColumn: "IdEmpleado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Empleados_IdEmpleado",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdEmpleado",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IdEmpleado",
                table: "Usuarios");
        }
    }
}

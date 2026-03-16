using CrudAPI.DTOs;
using CrudAPI.Entities;
using CrudAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoService _empleadoService;
        public EmpleadoController(EmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }


        [HttpGet]
        [Route("lista")]
        [Authorize]
        public async Task<ActionResult<List<EmpleadoDTO>>> Get()
        {
            return Ok(await _empleadoService.GetAllEmpleados());
        }

        [HttpGet]
        [Route("buscar/{id}")]
        [Authorize]
        public async Task<ActionResult<EmpleadoDTO>> Get(int id)
        {
            var result = await _empleadoService.GetEmpleadoFromID(id);
            return Ok(result != null ? result : "Empleado no encontrado");
        }

        [HttpPost]
        [Route("Agregar")]
        [Authorize(Roles = StaticRoles.Admin)]
        public async Task<ActionResult<EmpleadoDTO>> Agregar(EmpleadoDTO empleadoDTO)
        {
            return Ok(await _empleadoService.AddEmpleado(empleadoDTO));
        }

        [HttpPut]
        [Route("Editar")]
        [Authorize(Roles = StaticRoles.Admin)]
        public async Task<ActionResult> Editar(EmpleadoDTO empleadoDTO)
        {
            return Ok(await _empleadoService.EditEmpleado(empleadoDTO));
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        [Authorize(Roles = StaticRoles.Admin)]
        public async Task<ActionResult> Eliminar(int id)
        {
            return Ok(await _empleadoService.RemoveEmpleadoFromID(id));
        }

    }
}

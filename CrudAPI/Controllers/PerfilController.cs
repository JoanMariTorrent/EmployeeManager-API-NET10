using Microsoft.AspNetCore.Mvc;
using CrudAPI.DTOs;
using CrudAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace CrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly PerfilService _perfilService;
        public PerfilController(PerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet]
        [Authorize]
        [Route("lista")]
        public async Task<ActionResult<List<PerfilDTO>>> GetTask()
        {
            return Ok(await _perfilService.GetPerfiles());
        }
    }

    
}

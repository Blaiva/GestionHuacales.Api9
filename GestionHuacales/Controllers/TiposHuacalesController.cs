using GestionHuacales.Models;
using GestionHuacales.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposHuacalesController(EntradasHuacalesService service) : ControllerBase
    {
        // GET: api/<TiposHuacalesController>
        [HttpGet]
        public async Task<TiposHuacalesDTO[]> Get()
        {
            return await service.ListarTipos();
        }

    }
}

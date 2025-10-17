using GestionHuacales.Models;
using GestionHuacales.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GestionHuacales.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EntradasHuacalesController(EntradasHuacalesService service) : ControllerBase
{
    // GET: api/<EntradasHuacalesController>
    [HttpGet]
    public async Task<EntradasHuacalesDTO[]> Get()
    {
        return await service.Listar(h => true);
    }

    // POST api/<EntradasHuacalesController>
    [HttpPost]
    public async Task Post([FromBody] EntradasHuacalesDTO entradaHuacales)
    {
        var huacales = new EntradasHuacales
        {
            Fecha = DateTime.Now,
            NombreCliente = entradaHuacales.NombreCliente,
            Cantidad = entradaHuacales.Huacales.Sum(h => h.Cantidad),
            Importe = entradaHuacales.Huacales.Sum(h => h.Cantidad * h.Precio),
            EntradasHuacalesDetalle = entradaHuacales.Huacales.Select(h => new EntradasHuacalesDetalle
            {
                TipoId = h.TipoId,
                Cantidad = h.Cantidad,
                Precio = h.Precio,
            }).ToArray()
        };
        await service.Guardar(huacales);
    }

    // PUT api/<EntradasHuacalesController>/5
    [HttpPut("{id}")]
    public async Task Put(int id, EntradasHuacalesDTO entradaHuacales)
    {
        var huacales = new EntradasHuacales
        {
            IdEntrada = id,
            Fecha = DateTime.Now,
            NombreCliente = entradaHuacales.NombreCliente,
            Cantidad = entradaHuacales.Huacales.Sum(h => h.Cantidad),
            Importe = entradaHuacales.Huacales.Sum(h => h.Cantidad * h.Precio),
            EntradasHuacalesDetalle = entradaHuacales.Huacales.Select(h => new EntradasHuacalesDetalle
            {
                TipoId = h.TipoId,
                Cantidad = h.Cantidad,
                Precio = h.Precio,
            }).ToArray()
        };
        await service.Guardar(huacales);
    }

    // DELETE api/<EntradasHuacalesController>/5
    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await service.Eliminar(id);
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Models;

public class EntradasHuacalesDTO
{
    public string? NombreCliente { get; set; }
    public EntradasHuacalesDetalleDTO[] Huacales { get; set; } = [];
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GestionHuacales.Models;

public class EntradasHuacalesDetalle
{
    [Key]
    public int DetalleId { get; set; }

    public int EntradaId { get; set; }

    public int TipoId { get; set; }

    public int Cantidad { get; set; }

    public double Precio { get; set; }
}

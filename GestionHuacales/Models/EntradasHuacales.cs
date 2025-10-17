using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionHuacales.Models;

public class EntradasHuacales
{
    [Key]
    public int IdEntrada { get; set; }

    [Required(ErrorMessage = "La fecha de entrada es requerida")]
    public DateTime Fecha { get; set; }

    [StringLength(50, ErrorMessage = "El tamaño del nombre debe ser menor a 50 caracteres")]
    public string? NombreCliente { get; set; }

    [Required(ErrorMessage = "La cantidad de huacales es requerida")]
    [Range(1, int.MaxValue, ErrorMessage = "La cantidad de huacales debe ser mayor que cero")]
    public int Cantidad { get; set; }

    [Required(ErrorMessage = "El precio de los huacales es requerido")]
    [Range(0.01, int.MaxValue, ErrorMessage = "El importe debe ser mayor que cero")]
    public double Importe { get; set; }

    [ForeignKey("IdEntrada")]
    public virtual ICollection<EntradasHuacalesDetalle> EntradasHuacalesDetalle { get; set; } = new List<EntradasHuacalesDetalle>();
}

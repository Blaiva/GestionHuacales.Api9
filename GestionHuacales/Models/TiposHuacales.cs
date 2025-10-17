using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Models;

public class TiposHuacales
{
    [Key]
    public int TipoId { get; set; }

    [Required]
    [StringLength(50, ErrorMessage = "La descripcion debe tener de menos de 50 caracteres")]
    public string Descripcion { get; set; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "La existencia debe ser mayor que cero")]
    public int Existencia { get; set; }
}

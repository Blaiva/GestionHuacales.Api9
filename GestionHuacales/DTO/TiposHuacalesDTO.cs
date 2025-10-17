using System.ComponentModel.DataAnnotations;

namespace GestionHuacales.Models;

public class TiposHuacalesDTO
{
    public int TipoId { get; set; }

    public string Descripcion { get; set; }

    public int Existencia { get; set; }
}

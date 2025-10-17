using GestionHuacales.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionHuacales.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<EntradasHuacales> EntradasHuacales { get; set; }
    public DbSet<EntradasHuacalesDetalle> EntradasHuacalesDetalles { get; set; }
    public DbSet<TiposHuacales> TiposHuacales { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TiposHuacales>().HasData(
            new List<TiposHuacales>()
            {
                new()
                {
                    TipoId = 1,
                    Descripcion = "Rojo Pequeño"
                },
                new()
                {
                    TipoId = 2,
                    Descripcion = "Verde Pequeño"
                },
                new()
                {
                    TipoId = 3,
                    Descripcion = "Rojo Mediano"
                },
                new()
                {
                    TipoId = 4,
                    Descripcion = "Verde Mediano"
                },
                new()
                {
                    TipoId = 5,
                    Descripcion = "Rojo Grande"
                },
                 new()
                {
                    TipoId = 6,
                    Descripcion = "Verde Grande"
                },
            }
        );
        base.OnModelCreating(modelBuilder);
    }
}
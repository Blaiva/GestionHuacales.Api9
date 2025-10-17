using System.Linq.Expressions;
using GestionHuacales.DAL;
using GestionHuacales.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace GestionHuacales.Services;

public class EntradasHuacalesService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<bool> Existe(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.AnyAsync(e => e.IdEntrada == idEntrada);
    }

    public async Task AfectarExistencia(EntradasHuacalesDetalle[] detalle, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach(var item in detalle)
        {
            var tipoHuacal = await contexto.TiposHuacales.FirstOrDefaultAsync(t => t.TipoId == item.TipoId);
            if (tipoOperacion == TipoOperacion.Suma)
                tipoHuacal.Existencia += item.Cantidad;
            else
                tipoHuacal.Existencia -= item.Cantidad;
            await contexto.SaveChangesAsync();
        }
    }

    public async Task<bool> Insertar(EntradasHuacales entradaHuacal)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.EntradasHuacales.Add(entradaHuacal);
        await AfectarExistencia(entradaHuacal.EntradasHuacalesDetalle.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(EntradasHuacales entradaHuacal)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var original = await contexto.EntradasHuacales
            .Include(e => e.EntradasHuacalesDetalle)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.IdEntrada == entradaHuacal.IdEntrada);

        if (original == null) return false;

        await AfectarExistencia(original.EntradasHuacalesDetalle.ToArray(), TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalles.RemoveRange(original.EntradasHuacalesDetalle);

        contexto.Update(entradaHuacal);

        await AfectarExistencia(entradaHuacal.EntradasHuacalesDetalle.ToArray(), TipoOperacion.Suma);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Guardar(EntradasHuacales entradaHuacal)
    {
        if(!await Existe(entradaHuacal.IdEntrada))
            return await Insertar(entradaHuacal);
        else
            return await Modificar(entradaHuacal);
    }

    public async Task<bool> Eliminar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var entrada = await Buscar(idEntrada);

        await AfectarExistencia(entrada.EntradasHuacalesDetalle.ToArray(), TipoOperacion.Resta);
        contexto.EntradasHuacalesDetalles.RemoveRange(entrada.EntradasHuacalesDetalle);
        contexto.EntradasHuacales.Remove(entrada);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<EntradasHuacales?> Buscar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.Include(e => e.EntradasHuacalesDetalle).FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);
    }

    public async Task<EntradasHuacalesDTO[]> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
           .Where(criterio)
           .Select(h => new EntradasHuacalesDTO
           {
               NombreCliente = h.NombreCliente
           })
           .ToArrayAsync();
    }

    public async Task<TiposHuacalesDTO[]> ListarTipos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales.Where(t => t.TipoId > 0).Select(t => new TiposHuacalesDTO
        {
            TipoId = t.TipoId,
            Descripcion = t.Descripcion,
            Existencia = t.Existencia
        }).ToArrayAsync();
    }
}
public enum TipoOperacion
{
    Suma = 1,
    Resta = 2
}
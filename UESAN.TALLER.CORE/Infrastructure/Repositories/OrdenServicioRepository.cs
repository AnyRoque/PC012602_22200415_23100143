using Microsoft.EntityFrameworkCore;
using UESAN.TALLER.CORE.Core.Entities;
using UESAN.TALLER.CORE.Core.Interfaces;
using UESAN.TALLER.CORE.Infrastructure.Data;

namespace UESAN.TALLER.CORE.Infrastructure.Repositories
{
    public class OrdenServicioRepository : IOrdenServicioRepository
    {
        private readonly TallerDbContext _dbContext;

        public OrdenServicioRepository(TallerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<OrdenServicio>> GetOrdenes()
        {
            return await _dbContext
                        .OrdenServicio
                        .Include(o => o.Vehiculo)
                            .ThenInclude(v => v.Cliente)
                        .Include(o => o.TipoServicio)
                        .AsNoTracking()
                        .OrderByDescending(o => o.FechaIngreso)
                        .ToListAsync();
        }

        public async Task<OrdenServicio?> GetOrdenById(int id)
        {
            return await _dbContext
                        .OrdenServicio
                        .Include(o => o.Vehiculo)
                            .ThenInclude(v => v.Cliente)
                        .Include(o => o.TipoServicio)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> CreateOrden(OrdenServicio orden)
        {
            await _dbContext.OrdenServicio.AddAsync(orden);
            await _dbContext.SaveChangesAsync();
            return orden.Id; // Id generado por IDENTITY
        }

        public async Task<bool> UpdateOrden(OrdenServicio orden)
        {
            var existing = await _dbContext
                                .OrdenServicio
                                .FirstOrDefaultAsync(o => o.Id == orden.Id);
            if (existing == null) return false;

            existing.DescripcionProblema = orden.DescripcionProblema;
            existing.CostoEstimado = orden.CostoEstimado;
            existing.Estado = orden.Estado;
            existing.VehiculoId = orden.VehiculoId;
            existing.TipoServicioId = orden.TipoServicioId;

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrden(int id)
        {
            var existing = await _dbContext
                                .OrdenServicio
                                .FirstOrDefaultAsync(o => o.Id == id);
            if (existing == null) return false;

            _dbContext.OrdenServicio.Remove(existing);
            var rows = await _dbContext.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> ExisteVehiculo(int vehiculoId)
        {
            return await _dbContext.Vehiculo.AnyAsync(v => v.Id == vehiculoId);
        }

        public async Task<TipoServicio?> GetTipoServicio(int tipoServicioId)
        {
            return await _dbContext
                        .TipoServicio
                        .AsNoTracking()
                        .FirstOrDefaultAsync(t => t.Id == tipoServicioId);
        }
    }
}

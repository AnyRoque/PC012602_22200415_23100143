using UESAN.TALLER.CORE.Core.Entities;

namespace UESAN.TALLER.CORE.Core.Interfaces
{
    public interface IOrdenServicioRepository
    {
        Task<IEnumerable<OrdenServicio>> GetOrdenes();
        Task<OrdenServicio?> GetOrdenById(int id);
        Task<int> CreateOrden(OrdenServicio orden);
        Task<bool> UpdateOrden(OrdenServicio orden);
        Task<bool> DeleteOrden(int id);
        Task<bool> ExisteVehiculo(int vehiculoId);
        Task<TipoServicio?> GetTipoServicio(int tipoServicioId);
    }
}

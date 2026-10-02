using UESAN.TALLER.CORE.Core.DTOs;

namespace UESAN.TALLER.CORE.Core.Interfaces
{
    public interface IOrdenServicioService
    {
        Task<IEnumerable<OrdenServicioDTO>> GetOrdenes();
        Task<OrdenServicioDTO?> GetOrdenById(int id);
        Task<OrdenServicioDTO?> CreateOrden(OrdenServicioCreateDTO dto);
        Task<bool> UpdateOrden(OrdenServicioUpdateDTO dto);
        Task<bool> DeleteOrden(int id);
        Task<string?> ValidarReferencias(int vehiculoId, int tipoServicioId);
    }
}

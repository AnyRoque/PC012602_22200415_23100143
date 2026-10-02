using UESAN.TALLER.CORE.Core.DTOs;
using UESAN.TALLER.CORE.Core.Entities;
using UESAN.TALLER.CORE.Core.Interfaces;

namespace UESAN.TALLER.CORE.Infrastructure.Services
{
    public class OrdenServicioService : IOrdenServicioService
    {
        private const string EstadoInicial = "Pendiente";
        private readonly IOrdenServicioRepository _ordenRepository;

        public OrdenServicioService(IOrdenServicioRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public async Task<IEnumerable<OrdenServicioDTO>> GetOrdenes()
        {
            var ordenes = await _ordenRepository.GetOrdenes();
            return ordenes.Select(MapToDTO).ToList();
        }

        public async Task<OrdenServicioDTO?> GetOrdenById(int id)
        {
            var orden = await _ordenRepository.GetOrdenById(id);
            if (orden == null) return null;
            return MapToDTO(orden);
        }

        public async Task<OrdenServicioDTO?> CreateOrden(OrdenServicioCreateDTO dto)
        {
            var tipoServicio = await _ordenRepository.GetTipoServicio(dto.TipoServicioId);
            if (tipoServicio == null) return null;

            var orden = new OrdenServicio
            {
                FechaIngreso = DateTime.Now,
                DescripcionProblema = dto.DescripcionProblema.Trim(),
                // Regla de negocio: si no envían costo, se usa el precio base del servicio
                CostoEstimado = dto.CostoEstimado ?? tipoServicio.PrecioBase,
                Estado = EstadoInicial,
                VehiculoId = dto.VehiculoId,
                TipoServicioId = dto.TipoServicioId
            };

            var id = await _ordenRepository.CreateOrden(orden);
            return await GetOrdenById(id);
        }

        public async Task<bool> UpdateOrden(OrdenServicioUpdateDTO dto)
        {
            var orden = new OrdenServicio
            {
                Id = dto.Id,
                DescripcionProblema = dto.DescripcionProblema.Trim(),
                CostoEstimado = dto.CostoEstimado,
                Estado = dto.Estado,
                VehiculoId = dto.VehiculoId,
                TipoServicioId = dto.TipoServicioId
            };
            return await _ordenRepository.UpdateOrden(orden);
        }

        public async Task<bool> DeleteOrden(int id)
        {
            return await _ordenRepository.DeleteOrden(id);
        }

        // Devuelve un mensaje de error si alguna FK no existe, o null si todo está bien
        public async Task<string?> ValidarReferencias(int vehiculoId, int tipoServicioId)
        {
            if (!await _ordenRepository.ExisteVehiculo(vehiculoId))
                return $"No existe el vehículo con Id {vehiculoId}";

            if (await _ordenRepository.GetTipoServicio(tipoServicioId) == null)
                return $"No existe el tipo de servicio con Id {tipoServicioId}";

            return null;
        }

        private static OrdenServicioDTO MapToDTO(OrdenServicio o)
        {
            return new OrdenServicioDTO
            {
                Id = o.Id,
                FechaIngreso = o.FechaIngreso,
                DescripcionProblema = o.DescripcionProblema,
                CostoEstimado = o.CostoEstimado,
                Estado = o.Estado,
                Vehiculo = new VehiculoListDTO
                {
                    Id = o.Vehiculo?.Id ?? o.VehiculoId,
                    Placa = o.Vehiculo?.Placa ?? string.Empty,
                    Marca = o.Vehiculo?.Marca ?? string.Empty,
                    Modelo = o.Vehiculo?.Modelo ?? string.Empty,
                    Cliente = o.Vehiculo?.Cliente == null
                        ? string.Empty
                        : $"{o.Vehiculo.Cliente.Nombres} {o.Vehiculo.Cliente.Paterno} {o.Vehiculo.Cliente.Materno}"
                },
                TipoServicio = new TipoServicioListDTO
                {
                    Id = o.TipoServicio?.Id ?? o.TipoServicioId,
                    Nombre = o.TipoServicio?.Nombre ?? string.Empty,
                    PrecioBase = o.TipoServicio?.PrecioBase ?? 0
                }
            };
        }
    }
}

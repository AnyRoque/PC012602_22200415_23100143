using System.ComponentModel.DataAnnotations;

namespace UESAN.TALLER.CORE.Core.DTOs
{
    // DTO de salida (lo que devuelve la API)
    public class OrdenServicioDTO
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string DescripcionProblema { get; set; } = string.Empty;
        public decimal CostoEstimado { get; set; }
        public string Estado { get; set; } = string.Empty;
        public VehiculoListDTO Vehiculo { get; set; } = new();
        public TipoServicioListDTO TipoServicio { get; set; } = new();
    }

    public class VehiculoListDTO
    {
        public int Id { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
    }

    public class TipoServicioListDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public decimal PrecioBase { get; set; }
    }

    // DTO para crear (sin Id, sin FechaIngreso: la genera el sistema)
    public class OrdenServicioCreateDTO
    {
        [Required(ErrorMessage = "La descripción del problema es obligatoria")]
        [StringLength(255)]
        public string DescripcionProblema { get; set; } = string.Empty;

        // Opcional: si no se envía, se usa el PrecioBase del tipo de servicio
        [Range(0, 999999.99, ErrorMessage = "El costo no puede ser negativo")]
        public decimal? CostoEstimado { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "VehiculoId es obligatorio")]
        public int VehiculoId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "TipoServicioId es obligatorio")]
        public int TipoServicioId { get; set; }
    }

    // DTO para actualizar
    public class OrdenServicioUpdateDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string DescripcionProblema { get; set; } = string.Empty;

        [Range(0, 999999.99, ErrorMessage = "El costo no puede ser negativo")]
        public decimal CostoEstimado { get; set; }

        [Required]
        [RegularExpression("^(Pendiente|En Proceso|Terminado|Entregado)$",
            ErrorMessage = "Estado válido: Pendiente, En Proceso, Terminado o Entregado")]
        public string Estado { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int VehiculoId { get; set; }

        [Range(1, int.MaxValue)]
        public int TipoServicioId { get; set; }
    }
}

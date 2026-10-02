using Microsoft.AspNetCore.Mvc;
using UESAN.TALLER.CORE.Core.DTOs;
using UESAN.TALLER.CORE.Core.Interfaces;

namespace UESAN.TALLER.API.Controllers
{
    // Pregunta 5: controlador con patrón Repository + Service e inyección de dependencias
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenServicioController : ControllerBase
    {
        private readonly IOrdenServicioService _ordenService;

        public OrdenServicioController(IOrdenServicioService ordenService)
        {
            _ordenService = ordenService;
        }

        // GET: api/OrdenServicio
        [HttpGet]
        public async Task<IActionResult> GetOrdenes()
        {
            var ordenes = await _ordenService.GetOrdenes();
            return Ok(ordenes);
        }

        // GET: api/OrdenServicio/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrdenById(int id)
        {
            var orden = await _ordenService.GetOrdenById(id);
            if (orden == null) return NotFound();
            return Ok(orden);
        }

        // POST: api/OrdenServicio
        [HttpPost]
        public async Task<IActionResult> CreateOrden([FromBody] OrdenServicioCreateDTO dto)
        {
            var error = await _ordenService.ValidarReferencias(dto.VehiculoId, dto.TipoServicioId);
            if (error != null) return BadRequest(error);

            var orden = await _ordenService.CreateOrden(dto);
            if (orden == null) return BadRequest();

            return CreatedAtAction(nameof(GetOrdenById), new { id = orden.Id }, orden);
        }

        // PUT: api/OrdenServicio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrden(int id, [FromBody] OrdenServicioUpdateDTO dto)
        {
            if (id != dto.Id) return BadRequest("El Id de la URL no coincide con el del body.");

            var existing = await _ordenService.GetOrdenById(id);
            if (existing == null) return NotFound();

            var error = await _ordenService.ValidarReferencias(dto.VehiculoId, dto.TipoServicioId);
            if (error != null) return BadRequest(error);

            await _ordenService.UpdateOrden(dto);
            return NoContent();
        }

        // DELETE: api/OrdenServicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrden(int id)
        {
            var result = await _ordenService.DeleteOrden(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}

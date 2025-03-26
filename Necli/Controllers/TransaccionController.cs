using Microsoft.AspNetCore.Mvc;
using Necli.Entidades;

namespace Necli.Presentacion.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly ITransaccionService _transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        // Realizar una nueva transacción
        [HttpPost]
        public IActionResult RealizarTransaccion([FromBody] Transaccion transaccion)
        {
            try
            {
                var transaccionRealizada = _transaccionService.RealizarTransaccion(transaccion);
                return CreatedAtAction(nameof(ConsultarTransacciones), new { NumeroCuenta = transaccion.NumeroCuentaOrigen }, transaccionRealizada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // Consultar transacciones por número de cuenta y rango de fechas
        [HttpGet("{NumeroCuenta}/{desde?}/{hasta?}")]
        public IActionResult ConsultarTransacciones(string NumeroCuenta, DateTime? desde = null, DateTime? hasta = null)
        {
            var transacciones = _transaccionService.ConsultarTransacciones(NumeroCuenta, desde, hasta);
            if (transacciones == null)
            {
                return NotFound(new { mensaje = "No se encontraron transacciones." });
            }

            return Ok(transacciones);
        }

    }
}

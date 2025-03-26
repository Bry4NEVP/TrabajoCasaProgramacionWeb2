using Microsoft.AspNetCore.Mvc;
using Necli.Entidades;

namespace Necli.Presentacion.Controllers
{
    public class CuentController : Controller
    {
        private readonly ICuentaService _cuentaService;

        public CuentaController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        // Crear una cuenta nueva
        [HttpPost]
        public IActionResult CrearCuenta([FromBody] Cuenta cuenta)
        {
            try
            {
                var cuentaCreada = _cuentaService.CrearCuenta(cuenta);
                return CreatedAtAction(nameof(ConsultarCuenta), new { numero = cuentaCreada.Numero }, cuentaCreada);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // Consultar una cuenta por número
        [HttpGet("{numero}")]
        public IActionResult ConsultarCuenta(string numero)
        {
            var cuenta = _cuentaService.ConsultarCuenta(numero);
            if (cuenta == null)
            {
                return NotFound(new { mensaje = "Cuenta no encontrada." });
            }

            return Ok(cuenta);
        }

        // Eliminar una cuenta (solo si saldo < 50,000 COP)
        [HttpDelete("{numero}")]
        public IActionResult EliminarCuenta(string numero)
        {
            try
            {
                bool eliminada = _cuentaService.EliminarCuenta(numero);
                if (!eliminada)
                {
                    return BadRequest(new { mensaje = "No se puede eliminar la cuenta. Verifique el saldo." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

    }

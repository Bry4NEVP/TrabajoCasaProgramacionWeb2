using Microsoft.AspNetCore.Mvc;
using Necli.Entidades;
using Necli.Logica;
using Necli.Logica.Dto;

namespace Necli.Presentacion.Controllers
{
    public class CuentaController : Controller
    {
        private readonly CuentaService _cuentaService = new();

        [HttpPost]
        public IActionResult CrearCuenta([FromBody] CrearCuentaDto cuentaDto)
        {
            try
            {
                var cuentaCreada = _cuentaService.CrearCuenta(cuentaDto);
                return CreatedAtAction(nameof(ConsultarCuenta), new { numero = cuentaCreada.Numero }, cuentaCreada);
            }
            catch (Exception)
            {
                return BadRequest(new { mensaje = "No se pudo crear la cuenta. Verifique los datos ingresados." });
            }
        }

        [HttpGet("{numero}")]
        public IActionResult ConsultarCuenta(string numero)
        {
            try
            {
                var cuenta = _cuentaService.ConsultarCuenta(numero);
                if (cuenta == null)
                {
                    return NotFound(new { mensaje = "Cuenta no encontrada." });
                }
                return Ok(cuenta);
            }
            catch (Exception)
            {
                return BadRequest(new { mensaje = "Ocurrió un error al consultar la cuenta." });
            }
        }

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
            catch (Exception)
            {
                return BadRequest(new { mensaje = "Error al intentar eliminar la cuenta." });
            }
        }
    }
}

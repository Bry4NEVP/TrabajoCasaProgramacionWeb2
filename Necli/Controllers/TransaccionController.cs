using Microsoft.AspNetCore.Mvc;
using Necli.Entidades;
using Necli.Logica; // Agrega el espacio de nombres del servicio
using Necli.Logica.Dto; // Agrega el espacio de nombres de los DTOs


namespace Necli.Presentacion.Controllers
{
    public class TransaccionController : Controller
    {
        private readonly TransaccionService _transaccionService = new();

        [HttpPost]
        public IActionResult RealizarTransaccion([FromBody] CrearTransaccionDto transaccionDto)
        {
            try
            {
                var transaccion = _transaccionService.RealizarTransaccion(transaccionDto);
                return Ok(transaccion);
            }
            catch (Exception)
            {
                return BadRequest(new { mensaje = "No se pudo realizar la transacción. Verifique los datos." });
            }
        }

    }
}

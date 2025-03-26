using Microsoft.AspNetCore.Mvc;

namespace Necli.Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Consultar usuario por identificación
        [HttpGet("{identificacion}")]
        public IActionResult ConsultarUsuario(string identificacion)
        {
            var usuario = _usuarioService.ConsultarUsuario(identificacion);
            if (usuario == null)
            {
                return NotFound(new { mensaje = "Usuario no encontrado." });
            }

            return Ok(usuario);
        }

        // Actualizar datos del usuario
        [HttpPut("{identificacion}")]
        public IActionResult ActualizarUsuario(string identificacion, [FromBody] Usuario usuario)
        {
            if (identificacion != usuario.Identificacion)
            {
                return BadRequest(new { mensaje = "La identificación no coincide." });
            }

            try
            {
                bool actualizado = _usuarioService.ActualizarUsuario(usuario);
                if (!actualizado)
                {
                    return BadRequest(new { mensaje = "No se pudo actualizar el usuario." });
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}

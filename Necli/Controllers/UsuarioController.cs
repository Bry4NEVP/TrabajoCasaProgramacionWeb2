using Microsoft.AspNetCore.Mvc;
using Necli.Entidades;
using Necli.Logica;
using Necli.Logica.Dto;

namespace Necli.Presentacion.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService = new();

        // Consultar usuario por identificación
        [HttpGet("{identificacion}")]
        public IActionResult ConsultarUsuario(string identificacion)
        {
            try
            {
                var usuario = _usuarioService.ObtenerUsuario(identificacion);
                return Ok(usuario);
            }
            catch (Exception)
            {
                return BadRequest(new { mensaje = "Ocurrió un error al procesar la solicitud. Intente nuevamente más tarde." });
            }
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
                var usuarioDto = new UsuarioDto
                {
                    Identificacion = usuario.Identificacion,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Email = usuario.Email,
                    NumeroTelefono = usuario.NumeroTelefono
                };

                bool actualizado = _usuarioService.ActualizarUsuario(usuarioDto);
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

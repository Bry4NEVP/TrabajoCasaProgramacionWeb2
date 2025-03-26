using Necli.Entidades;
using Necli.Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Necli.Logica
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public bool RegistrarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Identificacion) ||
                string.IsNullOrWhiteSpace(usuario.Nombres) ||
                string.IsNullOrWhiteSpace(usuario.Apellidos) ||
                string.IsNullOrWhiteSpace(usuario.Email) ||
                string.IsNullOrWhiteSpace(usuario.NumeroTelefono))
            {
                throw new ArgumentException("Todos los campos son obligatorios.");
            }

            if (!Regex.IsMatch(usuario.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("El correo electrónico no tiene un formato válido.");
            }

            if (!Regex.IsMatch(usuario.NumeroTelefono, @"^\d{10}$"))
            {
                throw new ArgumentException("El número de teléfono debe tener 10 dígitos.");
            }

            if (_usuarioRepository.ExisteUsuario(usuario.Identificacion, usuario.Email, usuario.NumeroTelefono))
            {
                throw new InvalidOperationException("El usuario ya está registrado con la misma identificación, email o teléfono.");
            }

            // 5️⃣ Encriptar la contraseña antes de guardarla
            usuario.Contrasena = EncriptarContrasena(usuario.Contrasena);

            // 6️⃣ Registrar el usuario
            return _usuarioRepository.RegistrarUsuario(usuario);
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            // 1️⃣ Validaciones
            if (string.IsNullOrWhiteSpace(usuario.Identificacion))
            {
                throw new ArgumentException("La identificación es obligatoria.");
            }

            // 2️⃣ Si cambia el email o el teléfono, validar la unicidad
            var usuarioExistente = _usuarioRepository.ObtenerPorIdentificacion(usuario.Identificacion);
            if (usuarioExistente == null)
            {
                throw new ArgumentException("El usuario no existe.");
            }

            if (usuario.Email != usuarioExistente.Email || usuario.NumeroTelefono != usuarioExistente.NumeroTelefono)
            {
                if (_usuarioRepository.ExisteUsuario("", usuario.Email, usuario.NumeroTelefono))
                {
                    throw new InvalidOperationException("El nuevo correo o número de teléfono ya está en uso.");
                }
            }

            // 3️⃣ Encriptar contraseña si ha cambiado
            if (!string.IsNullOrWhiteSpace(usuario.Contrasena))
            {
                usuario.Contrasena = EncriptarContrasena(usuario.Contrasena);
            }
            else
            {
                usuario.Contrasena = usuarioExistente.Contrasena; // Mantener la actual
            }

            // 4️⃣ Actualizar usuario
            return _usuarioRepository.ActualizarUsuario(usuario);
        }

        private string EncriptarContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                StringBuilder resultado = new StringBuilder();
                foreach (byte b in bytes)
                {
                    resultado.Append(b.ToString("x2"));
                }
                return resultado.ToString();
            }
        }

    }
}

using Necli.Entidades;
using Necli.Logica.Dto;
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

            public UsuarioDto ObtenerUsuario(string identificacion)
            {
                var usuario = _usuarioRepository.ObtenerPorIdentificacion(identificacion);
                if (usuario == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                return new UsuarioDto
                {
                    Identificacion = usuario.Identificacion,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Email = usuario.Email,
                    NumeroTelefono = usuario.NumeroTelefono
                };
            }

        public bool ActualizarUsuario(UsuarioDto usuarioDto)
        {
            if (string.IsNullOrWhiteSpace(usuarioDto.Nombres) || string.IsNullOrWhiteSpace(usuarioDto.Apellidos))
            {
                throw new Exception("Los nombres y apellidos no pueden estar vacíos");
            }

            if (!Regex.IsMatch(usuarioDto.Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                throw new Exception("El formato del correo electrónico no es válido");
            }

            if (!Regex.IsMatch(usuarioDto.NumeroTelefono, @"^\d{10}$"))
            {
                throw new Exception("El número de teléfono debe tener 10 dígitos");
            }

            // Convertir UsuarioDto a Usuario antes de pasarlo al repositorio
            var usuario = new Usuario
            {
                Identificacion = usuarioDto.Identificacion,
                Nombres = usuarioDto.Nombres,
                Apellidos = usuarioDto.Apellidos,
                Email = usuarioDto.Email,
                NumeroTelefono = usuarioDto.NumeroTelefono
            };

            return _usuarioRepository.ActualizarUsuario(usuario);
        }
    }
    }


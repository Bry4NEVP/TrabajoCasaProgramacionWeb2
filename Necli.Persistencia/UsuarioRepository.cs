using Necli.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Persistencia
{
    public class UsuarioRepository
    {
        private readonly string _cadena_conexion = "Server=SJLBA01SALAA18\\SQLEXPRESS; Database=VehiGestion;User ID=sa;Password=cecar; TrustServerCertificate=True;";
        private string sql;
        public bool ActualizarUsuario(Usuario usuario)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                var sql = @"UPDATE Usuario SET 
                       Nombres = @Nombres, 
                       Apellidos = @Apellidos,
                       Email = @Email,
                       NumeroTelefono = @NumeroTelefono,
                       Contrasena = @Contrasena
                       WHERE Identificacion = @Identificacion";

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Identificacion", usuario.Identificacion);
                    comando.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                    comando.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                    comando.Parameters.AddWithValue("@Email", usuario.Email);
                    comando.Parameters.AddWithValue("@NumeroTelefono", usuario.NumeroTelefono);
                    comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);

                    conexion.Open();
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
        public Usuario ObtenerPorIdentificacion(string identificacion)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                var sql = "SELECT * FROM Usuario WHERE Identificacion = @Identificacion";

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Identificacion", identificacion);
                    conexion.Open();

                    using (var reader = comando.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Usuario
                            {
                                Identificacion = reader["Identificacion"].ToString(),
                                Contrasena = reader["Contrasena"].ToString(),
                                Nombres = reader["Nombres"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Email = reader["Email"].ToString(),
                                NumeroTelefono = reader["NumeroTelefono"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }


    }
}

using Necli.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Persistencia
{
    public class CuentaRepository
    {
        private readonly string _cadena_conexion = "Server=SJLBA01SALAA18\\SQLEXPRESS; Database=VehiGestion;User ID=sa;Password=cecar; TrustServerCertificate=True;";
        private string sql;

        public void CrearCuenta(Cuenta cuenta)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                string sql = @"INSERT INTO Cuentas (Numero, Saldo, FechaCreacion, UsuarioIdentificacion) 
                               VALUES (@Numero, @Saldo, @FechaCreacion, @UsuarioIdentificacion)";

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Numero", cuenta.Numero);
                    comando.Parameters.AddWithValue("@Saldo", cuenta.Saldo);
                    comando.Parameters.AddWithValue("@FechaCreacion", cuenta.FechaCreacion);
                    comando.Parameters.AddWithValue("@UsuarioIdentificacion", cuenta.UsuarioIdentificacion);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public Cuenta ConsultarCuenta(string numero)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                string sql = "SELECT * FROM Cuentas WHERE Numero = @Numero";

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Numero", numero);
                    conexion.Open();
                    var lector = comando.ExecuteReader();

                    if (lector.Read())
                    {
                        return new Cuenta
                        {
                            Numero = lector["Numero"].ToString(),
                            Saldo = Convert.ToDecimal(lector["Saldo"]),
                            FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"]),
                            UsuarioIdentificacion = lector["UsuarioIdentificacion"].ToString()
                        };
                    }
                }
            }
            return null;
        }
        public bool EliminarCuenta(string numero)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                string sql = "DELETE FROM Cuentas WHERE Numero = @Numero";

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Numero", numero);
                    conexion.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
        }
    }
}



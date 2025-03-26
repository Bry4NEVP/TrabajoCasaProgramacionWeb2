using Necli.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Persistencia
{
    public class CuentaRepository
    {
        private readonly string _cadena_conexion = "Server=SJLBA01SALAA18\\SQLEXPRESS; Database=VehiGestion;User ID=sa;Password=cecar; TrustServerCertificate=True;";
        private string sql;

        public bool RegistrarCuenta(Cuenta cuenta )
        {


            using (var conexion = new SqlConnection(_cadena_conexion))
            {

                sql = @"INSERT INTO Cuentas(Numero,Saldo,FechaCreacion) 
                    VALUES(@PNumero,@Saldo,@FechaCreacion)";


                using (var comando = new SqlCommand(sql, conexion))
                {


                    comando.Parameters.AddWithValue("@Numero", cuenta.Numero);
                    comando.Parameters.AddWithValue("@Saldo", cuenta.Saldo);
                    comando.Parameters.AddWithValue("@FechaCreacion", DateTime.Now);
                    comando.Parameters.AddWithValue("@UsuarioIdentificacion", cuenta.UsuarioIdentificacion);
                    conexion.Open();
                    comando.ExecuteNonQuery();

                }

            }


            return true;

        }

        public Cuenta ConsultarCuenta(string Numero)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                sql = "SELECT * FROM Cuentas WHERE Numero=@Numero";

                using (var comando = new SqlCommand(sql, conexion))
                {

                    comando.Parameters.AddWithValue("@Numero", Numero);
                    conexion.Open();
                    var lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        var cuenta = new Cuenta
                        {

                            Numero = lector["Numero"].ToString(),
                            Saldo = Convert.ToInt32(lector["Saldo"].ToString()),
                            FechaCreacion = DateTime.Parse(lector["FechaCreacion"].ToString())

                        };
                        return cuenta;

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

                    // Retorna true si se eliminó exactamente 1 fila
                    return filasAfectadas == 1;
                }
            }
        }


    }
}
}

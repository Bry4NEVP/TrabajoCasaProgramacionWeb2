using Necli.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Persistencia
{
    internal class TransaccionRepository
    {
        private readonly string _cadena_conexion = "Server=SJLBA01SALAA18\\SQLEXPRESS; Database=VehiGestion; User ID=sa; Password=cecar; TrustServerCertificate=True;";

        // Registrar una nueva transacción
        public bool RegistrarTransaccion(Transaccion transaccion)
        {
            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                var sql = @"INSERT INTO Transaccion (Numero, Fecha, NumeroCuentaOrigen, NumeroCuentaDestino, Monto, Tipo)
                        VALUES (@Numero, @Fecha, @NumeroCuentaOrigen, @NumeroCuentaDestino, @Monto, @Tipo)";

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@Numero", transaccion.Numero);
                    comando.Parameters.AddWithValue("@Fecha", transaccion.Fecha);
                    comando.Parameters.AddWithValue("@NumeroCuentaOrigen", transaccion.NumeroCuentaOrigen);
                    comando.Parameters.AddWithValue("@NumeroCuentaDestino", transaccion.NumeroCuentaDestino);
                    comando.Parameters.AddWithValue("@Monto", transaccion.Monto);
                    comando.Parameters.AddWithValue("@Tipo", transaccion.Tipo);

                    conexion.Open();
                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // Consultar transacciones por cuenta y rango de fechas
        public List<Transaccion> ConsultarTransacciones(string numeroCuenta, DateTime? desde, DateTime? hasta)
        {
            var transacciones = new List<Transaccion>();

            using (var conexion = new SqlConnection(_cadena_conexion))
            {
                var sql = @"SELECT Numero, Fecha, NumeroCuentaOrigen, NumeroCuentaDestino, Monto, Tipo
                        FROM Transaccion
                        WHERE (NumeroCuentaOrigen = @NumeroCuenta OR NumeroCuentaDestino = @NumeroCuenta)";

                if (desde.HasValue && hasta.HasValue)
                {
                    sql += " AND Fecha BETWEEN @Desde AND @Hasta";
                }

                using (var comando = new SqlCommand(sql, conexion))
                {
                    comando.Parameters.AddWithValue("@NumeroCuenta", numeroCuenta);
                    if (desde.HasValue && hasta.HasValue)
                    {
                        comando.Parameters.AddWithValue("@Desde", desde.Value);
                        comando.Parameters.AddWithValue("@Hasta", hasta.Value);
                    }

                    conexion.Open();
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transacciones.Add(new Transaccion
                            {
                                Numero = reader["Numero"].ToString(),
                                Fecha = Convert.ToDateTime(reader["Fecha"]),
                                NumeroCuentaOrigen = reader["NumeroCuentaOrigen"].ToString(),
                                NumeroCuentaDestino = reader["NumeroCuentaDestino"].ToString(),
                                Monto = Convert.ToDecimal(reader["Monto"]),
                                Tipo = reader["Tipo"].ToString()
                            });
                        }
                    }
                }
            }

            return transacciones;
        }

    }
}

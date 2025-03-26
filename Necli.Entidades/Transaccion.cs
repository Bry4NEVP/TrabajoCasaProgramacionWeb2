using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Entidades
{
    public class Transaccion
    {
        public string Numero { get; set; }  // Generado automáticamente por el sistema
        public DateTime Fecha { get; set; } = DateTime.Now;  // Se toma del sistema al crear
        public string NumeroCuentaOrigen { get; set; }
        public string NumeroCuentaDestino { get; set; }
        public decimal Monto { get; set; }
        public string Tipo { get; set; }  // "entrada" o "salida"

        public Transaccion(string numeroCuentaOrigen, string numeroCuentaDestino, decimal monto, string tipo)
        {
            if (monto < 1000 || monto > 5000000)
            {
                throw new ArgumentException("El monto debe estar entre $1,000 COP y $5,000,000 COP.");
            }

            Numero = Guid.NewGuid().ToString();  // Generación automática del número de transacción
            NumeroCuentaOrigen = numeroCuentaOrigen;
            NumeroCuentaDestino = numeroCuentaDestino;
            Monto = monto;
            Tipo = tipo;
        }


    }
}

using Necli.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Logica
{
     class TransaccionService
    {
        
        private readonly ICuentaService _cuentaService;
        private readonly ITransaccionService _transaccionService;

        public TransaccionService(ICuentaService cuentaService, ITransaccionService transaccionService)
        {
            _cuentaService = cuentaService;
            _transaccionService = transaccionService;
        }

        public Transaccion RealizarTransaccion(string numeroCuentaOrigen, string numeroCuentaDestino, decimal monto, string tipo)
        {
            // 1️⃣ Validación del monto permitido
            if (monto < 1000 || monto > 5000000)
            {
                throw new ArgumentException("El monto debe estar entre $1,000 COP y $5,000,000 COP.");
            }

            // 2️⃣ Validación de existencia de cuentas
            var cuentaOrigen = _cuentaService.ObtenerCuentaPorNumero(numeroCuentaOrigen);
            var cuentaDestino = _cuentaService.ObtenerCuentaPorNumero(numeroCuentaDestino);

            if (cuentaOrigen == null || cuentaDestino == null)
            {
                throw new ArgumentException("Una o ambas cuentas no existen.");
            }

            // 3️⃣ Validación de saldo en cuenta origen
            if (cuentaOrigen.Saldo < monto)
            {
                throw new InvalidOperationException("Saldo insuficiente en la cuenta origen.");
            }

            // 4️⃣ Crear la transacción
            var transaccion = new Transaccion
            {
                Numero = Guid.NewGuid().ToString(),  // Generar ID único
                Fecha = DateTime.Now,  // Fecha del sistema
                NumeroCuentaOrigen = numeroCuentaOrigen,
                NumeroCuentaDestino = numeroCuentaDestino,
                Monto = monto,
                Tipo = tipo
            };

            // 5️⃣ Aplicar la transacción (descontar y sumar saldo)
            cuentaOrigen.Saldo -= monto;
            cuentaDestino.Saldo += monto;

            // 6️⃣ Persistir cambios en base de datos
            _cuentaService.ActualizarCuenta(cuentaOrigen);
            _cuentaService.ActualizarCuenta(cuentaDestino);
            _transaccionService.GuardarTransaccion(transaccion);

            return transaccion;
        }
    }

}
}

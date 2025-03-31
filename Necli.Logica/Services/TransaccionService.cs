using Necli.Entidades;
using Necli.Logica.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Necli.Persistencia;

namespace Necli.Logica
{
     public class TransaccionService
    {

        private readonly TransaccionRepository _transaccionRepository = new();

        public Transaccion RealizarTransaccion(CrearTransaccionDto transaccionDto)
        {
            var transaccion = new Transaccion(
                transaccionDto.NumeroCuentaOrigen,
                transaccionDto.NumeroCuentaDestino,
                transaccionDto.Monto,
                transaccionDto.Tipo);

            _transaccionRepository.RegistrarTransaccion(transaccion);
            return transaccion;
        }
    }

}


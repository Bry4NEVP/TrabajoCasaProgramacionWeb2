using Necli.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Logica.Dto
{
    public class CuentaDto
    {
        public string Numero { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaCreacion { get; set; }

        public CuentaDto(Cuenta cuenta)
        {
            Numero = cuenta.Numero;
            Saldo = cuenta.Saldo;
            FechaCreacion = cuenta.FechaCreacion;
        }
    }
}

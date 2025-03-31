using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Logica.Dto
{
    public class CrearTransaccionDto
    {
        public string NumeroCuentaOrigen { get; set; }
        public string NumeroCuentaDestino { get; set; }
        public decimal Monto { get; set; }
        public string Tipo { get; set; }
    }
}

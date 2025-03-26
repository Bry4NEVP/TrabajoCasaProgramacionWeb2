using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Necli.Entidades
{
    public class Usuario
    {
        public string Identificacion { get; set; }
        public string Contrasena { get; set; } // Se almacenará encriptada
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public string NumeroTelefono { get; set; }

    }
}

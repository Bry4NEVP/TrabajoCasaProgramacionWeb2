using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Necli.Entidades
{
    public class Cuenta
    {
        public string Numero { get; set; } 

        public decimal Saldo { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [ForeignKey("Usuario")]
        public string UsuarioIdentificacion { get; set; }
        public virtual Usuario Usuario { get; set; }

        public bool PuedeEliminarse()
        {
            return Saldo <= 50000; // Condición de negocio aquí
        }

    }

}
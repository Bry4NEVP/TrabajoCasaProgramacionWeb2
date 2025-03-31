using Necli.Entidades;
using Necli.Logica.Dto;
using Necli.Persistencia;

namespace Necli.Logica
{
    public class CuentaService
    {
        private readonly CuentaRepository _cuentaRepository = new();

        public Cuenta CrearCuenta(CrearCuentaDto cuentaDto)
        {
            var cuenta = new Cuenta
            {
                Numero = cuentaDto.Numero,
                Saldo = cuentaDto.Saldo,
                FechaCreacion = DateTime.Now,
                UsuarioIdentificacion = cuentaDto.UsuarioIdentificacion
            };
            _cuentaRepository.CrearCuenta(cuenta);
            return cuenta;
        }

        public CuentaDto ConsultarCuenta(string numero)
        {
            var cuenta = _cuentaRepository.ConsultarCuenta(numero);
            return cuenta == null ? null : new CuentaDto(cuenta);
        }

        public bool EliminarCuenta(string numero)
        {
            var cuenta = _cuentaRepository.ConsultarCuenta(numero);
            if (cuenta == null || !cuenta.PuedeEliminarse())
            {
                return false;
            }
            return _cuentaRepository.EliminarCuenta(numero);
        }
    }
}

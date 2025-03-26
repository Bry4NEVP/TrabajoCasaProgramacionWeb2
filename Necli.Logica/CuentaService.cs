namespace Necli.Logica
{
    public class CuentaService
    {
        public (bool success, string message) EliminarCuentaConValidacion(string numeroCuenta)
        {
            try
            {
                // 1. Obtener la cuenta primero
                var cuenta = ObtenerCuenta(numeroCuenta); // Usando tu función ConsultarVehiculo

                if (cuenta == null)
                    return (false, "La cuenta no existe");

                // 2. Validar si se puede eliminar (delegado a la clase Cuenta)
                if (!cuenta.PuedeEliminarse())
                    return (false, "No se puede eliminar una cuenta con saldo mayor a $50,000");

                // 3. Si pasa validación, proceder a eliminar
                bool eliminado = EliminarCuenta(numeroCuenta);

                return eliminado
                    ? (true, "Cuenta eliminada exitosamente")
                    : (false, "No se pudo eliminar la cuenta");
            }
            catch (Exception ex)
            {
                // Loggear el error
                return (false, $"Error al eliminar cuenta: {ex.Message}");
            }
        }
    }
}
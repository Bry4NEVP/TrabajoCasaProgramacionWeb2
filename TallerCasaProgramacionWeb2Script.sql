-- Crear la base de datos
CREATE DATABASE VehiGestion;
-- Usar la base de datos
USE VehiGestion;
-- Entidad: Usuario
CREATE TABLE Usuario (
    Identificacion VARCHAR(20),
    Contrasena NVARCHAR(255),
    Nombres NVARCHAR(100),
    Apellidos NVARCHAR(100),
    Email NVARCHAR(100),
    NumeroTelefono VARCHAR(10)
);

-- Entidad: Cuenta
CREATE TABLE Cuenta (
    NumeroCuenta VARCHAR(20),
    IdentificacionUsuario VARCHAR(20),
    Saldo DECIMAL(18,2),
    FechaCreacion DATETIME
);

-- Entidad: Transaccion
CREATE TABLE Transaccion (
    Numero VARCHAR(36),
    Fecha DATETIME,
    NumeroCuentaOrigen VARCHAR(20),
    NumeroCuentaDestino VARCHAR(20),
    Monto DECIMAL(18,2),
    Tipo NVARCHAR(10)
);


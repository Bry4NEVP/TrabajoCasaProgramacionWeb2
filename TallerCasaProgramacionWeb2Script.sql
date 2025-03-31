-- Crear la base de datos
CREATE DATABASE NecliDB;

USE NecliDB;

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    Identificacion NVARCHAR(20) PRIMARY KEY,
    Contrasena NVARCHAR(255) NOT NULL,
    Nombres NVARCHAR(100) NOT NULL,
    Apellidos NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    NumeroTelefono NVARCHAR(10) UNIQUE NOT NULL
);

-- Tabla de Cuentas
CREATE TABLE Cuentas (
    Numero NVARCHAR(20) PRIMARY KEY,
    Saldo DECIMAL(18,2) NOT NULL CHECK (Saldo >= 0),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    UsuarioIdentificacion NVARCHAR(20) NOT NULL,
    FOREIGN KEY (UsuarioIdentificacion) REFERENCES Usuarios(Identificacion) ON DELETE CASCADE
);


-- Tabla de Transacciones
CREATE TABLE Transacciones (
    Numero UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Fecha DATETIME DEFAULT GETDATE(),
    NumeroCuentaOrigen NVARCHAR(20) NOT NULL,
    NumeroCuentaDestino NVARCHAR(20) NOT NULL,
    Monto DECIMAL(18,2) NOT NULL CHECK (Monto >= 1000 AND Monto <= 5000000),
    Tipo NVARCHAR(10) CHECK (Tipo IN ('entrada', 'salida')),
    FOREIGN KEY (NumeroCuentaOrigen) REFERENCES Cuentas(Numero),
    FOREIGN KEY (NumeroCuentaDestino) REFERENCES Cuentas(Numero)
);
-- Insertar Usuarios
INSERT INTO Usuarios (Identificacion, Contrasena, Nombres, Apellidos, Email, NumeroTelefono)
VALUES 
('1001', 'hashed_password_1', 'Juan', 'Pérez', 'juan.perez@example.com', '3110000001'),
('1002', 'hashed_password_2', 'María', 'López', 'maria.lopez@example.com', '3220000002'),
('1003', 'hashed_password_3', 'Carlos', 'Martínez', 'carlos.martinez@example.com', '3330000003');

-- Insertar Cuentas
INSERT INTO Cuentas (Numero, Saldo, UsuarioIdentificacion)
VALUES 
('C001', 200000, '1001'),
('C002', 500000, '1002'),
('C003', 150000, '1003');

-- Insertar Transacciones
INSERT INTO Transacciones (NumeroCuentaOrigen, NumeroCuentaDestino, Monto, Tipo)
VALUES 
('C001', 'C002', 50000, 'salida'),
('C002', 'C003', 80000, 'salida'),
('C003', 'C001', 100000, 'entrada');



-- =============================================
-- PREGUNTA 1: Base de datos - Taller Mecánico
-- =============================================
CREATE DATABASE TallerMecanico;
GO
USE TallerMecanico;
GO

-- Tabla: TipoServicio
CREATE TABLE TipoServicio (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    PrecioBase DECIMAL(10, 2) NOT NULL
);

-- Tabla: Cliente
CREATE TABLE Cliente (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Paterno VARCHAR(50) NOT NULL,
    Materno VARCHAR(50) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Correo VARCHAR(100) NULL,
    Telefono VARCHAR(20) NULL
);

-- Tabla: Vehiculo (Relación 1 a N con Cliente)
CREATE TABLE Vehiculo (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Placa VARCHAR(15) NOT NULL,
    Marca VARCHAR(50) NOT NULL,
    Modelo VARCHAR(50) NOT NULL,
    Anio INT NOT NULL,
    ClienteId INT NOT NULL,
    CONSTRAINT FK_Vehiculo_Cliente FOREIGN KEY (ClienteId) REFERENCES Cliente(Id)
);

-- Tabla: OrdenServicio (Relación con Vehículo y TipoServicio)
CREATE TABLE OrdenServicio (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FechaIngreso DATETIME NOT NULL DEFAULT GETDATE(),
    DescripcionProblema NVARCHAR(255) NOT NULL,
    CostoEstimado DECIMAL(10, 2) NOT NULL,
    Estado VARCHAR(50) NOT NULL,
    VehiculoId INT NOT NULL,
    TipoServicioId INT NOT NULL,
    CONSTRAINT FK_OrdenServicio_Vehiculo FOREIGN KEY (VehiculoId) REFERENCES Vehiculo(Id),
    CONSTRAINT FK_OrdenServicio_TipoServicio FOREIGN KEY (TipoServicioId) REFERENCES TipoServicio(Id)
);
GO

-- =============================================
-- Datos de prueba
-- =============================================
INSERT INTO TipoServicio (Nombre, PrecioBase) VALUES
('Cambio de aceite', 120.00),
('Afinamiento', 250.00),
('Revisión de frenos', 180.00);

INSERT INTO Cliente (Paterno, Materno, Nombres, Correo, Telefono) VALUES
('Pérez', 'García', 'Juan Carlos', 'juan.perez@mail.com', '987654321'),
('Ramos', 'Torres', 'María Elena', 'maria.ramos@mail.com', '912345678');

-- El cliente 1 tiene 2 vehículos (1 a N)
INSERT INTO Vehiculo (Placa, Marca, Modelo, Anio, ClienteId) VALUES
('ABC-123', 'Toyota', 'Corolla', 2018, 1),
('XYZ-789', 'Hyundai', 'Tucson', 2021, 1),
('DEF-456', 'Kia', 'Rio', 2019, 2);

INSERT INTO OrdenServicio (DescripcionProblema, CostoEstimado, Estado, VehiculoId, TipoServicioId) VALUES
('Ruido al frenar', 180.00, 'Pendiente', 1, 3),
('Mantenimiento de 10 000 km', 120.00, 'En Proceso', 3, 1);
GO

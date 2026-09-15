-- =============================================
-- RESERVA2 — Base de datos de NEGOCIO
-- SQL Server 2019+
-- Ejecutar DESPUÉS de Reserva2_Servicio.sql
-- =============================================

CREATE DATABASE Reserva2_Negocio;
GO
USE Reserva2_Negocio;
GO

CREATE TABLE Comercio (
    IdComercio      INT IDENTITY(1,1) PRIMARY KEY,
    Nombre          VARCHAR(150) NOT NULL,
    Slug            VARCHAR(150),
    Ubicacion       VARCHAR(300),
    LogoUrl         VARCHAR(500),
    Activo          BIT NOT NULL DEFAULT 1,
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    DVH             VARCHAR(500)
);

CREATE TABLE ElementoReservable (
    IdElemento      INT IDENTITY(1,1) PRIMARY KEY,
    Nombre          VARCHAR(150) NOT NULL,
    Descripcion     VARCHAR(500),
    Tipo            VARCHAR(50) NOT NULL,
    PrecioBase      DECIMAL(18,2) NOT NULL,
    Estado          VARCHAR(20) NOT NULL DEFAULT 'Activo',
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    IdComercio      INT NOT NULL,
    DVH             VARCHAR(500),
    CONSTRAINT FK_Elemento_Comercio FOREIGN KEY (IdComercio) REFERENCES Comercio(IdComercio)
);

CREATE TABLE ReglaDisponibilidad (
    IdReglaDisponibilidad INT IDENTITY(1,1) PRIMARY KEY,
    DiaSemana       VARCHAR(20) NOT NULL,
    HoraInicio      TIME NOT NULL,
    HoraFin         TIME NOT NULL,
    DuracionSlot    INT NOT NULL,
    PrecioOverride  DECIMAL(18,2),
    Activo          BIT NOT NULL DEFAULT 1,
    IdElemento      INT NOT NULL,
    CONSTRAINT FK_ReglaDisp_Elemento FOREIGN KEY (IdElemento) REFERENCES ElementoReservable(IdElemento)
);

CREATE TABLE Disponibilidad (
    IdDisponibilidad INT IDENTITY(1,1) PRIMARY KEY,
    Fecha           DATE NOT NULL,
    HoraInicio      TIME NOT NULL,
    HoraFin         TIME NOT NULL,
    Precio          DECIMAL(18,2) NOT NULL,
    Estado          VARCHAR(20) NOT NULL DEFAULT 'Libre',
    IdElemento      INT NOT NULL,
    DVH             VARCHAR(500),
    CONSTRAINT FK_Disp_Elemento FOREIGN KEY (IdElemento) REFERENCES ElementoReservable(IdElemento)
);

CREATE TABLE Reserva (
    IdReserva       INT IDENTITY(1,1) PRIMARY KEY,
    Estado          VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    FechaReserva    DATETIME NOT NULL DEFAULT GETDATE(),
    ConfirmadaEn    DATETIME,
    CanceladaEn     DATETIME,
    ExpiraEn        DATETIME,
    MotivoCancelacion VARCHAR(500),
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    IdDisponibilidad INT NOT NULL,
    DVH             VARCHAR(500),
    CONSTRAINT FK_Reserva_Disponibilidad FOREIGN KEY (IdDisponibilidad) REFERENCES Disponibilidad(IdDisponibilidad)
);

CREATE TABLE Pago (
    IdPago          INT IDENTITY(1,1) PRIMARY KEY,
    Estado          VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    FechaAcreditacion DATETIME,
    FechaPago       DATETIME,
    Monto           DECIMAL(18,2) NOT NULL,
    MpPaymentId     VARCHAR(100),
    MpPreferenceId  VARCHAR(100),
    IdReserva       INT NOT NULL,
    DVH             VARCHAR(500),
    CONSTRAINT FK_Pago_Reserva FOREIGN KEY (IdReserva) REFERENCES Reserva(IdReserva)
);

CREATE TABLE Sena (
    IdSena          INT IDENTITY(1,1) PRIMARY KEY,
    Estado          VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    FechaSena       DATETIME,
    MontoSena       DECIMAL(18,2) NOT NULL,
    IdPago          INT NOT NULL,
    CONSTRAINT FK_Sena_Pago FOREIGN KEY (IdPago) REFERENCES Pago(IdPago)
);

CREATE TABLE CBU (
    IdCBU           INT IDENTITY(1,1) PRIMARY KEY,
    Alias           VARCHAR(100),
    Cbu             VARCHAR(500) NOT NULL,
    Activo          BIT NOT NULL DEFAULT 1,
    IdComercio      INT NOT NULL,
    CONSTRAINT FK_CBU_Comercio FOREIGN KEY (IdComercio) REFERENCES Comercio(IdComercio)
);

CREATE TABLE ReglaNegocio (
    IdReglaNegocio  INT IDENTITY(1,1) PRIMARY KEY,
    Clave           VARCHAR(100) NOT NULL,
    Descripcion     VARCHAR(500),
    Valor           VARCHAR(200) NOT NULL,
    IdComercio      INT NOT NULL,
    CONSTRAINT FK_ReglaNeg_Comercio FOREIGN KEY (IdComercio) REFERENCES Comercio(IdComercio)
);

CREATE TABLE ListaEspera (
    IdListaEspera   INT IDENTITY(1,1) PRIMARY KEY,
    Estado          VARCHAR(30) NOT NULL DEFAULT 'Esperando',
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    NotificadoEn    DATETIME,
    Posicion        INT NOT NULL,
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    IdDisponibilidad INT NOT NULL,
    CONSTRAINT FK_ListaEspera_Disponibilidad FOREIGN KEY (IdDisponibilidad) REFERENCES Disponibilidad(IdDisponibilidad)
);

CREATE TABLE Notificacion (
    IdNotificacion  INT IDENTITY(1,1) PRIMARY KEY,
    Canal           VARCHAR(50) NOT NULL,
    Estado          VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    Tipo            VARCHAR(50),
    EnviadaEn       DATETIME,
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    IdReserva       INT,
    CONSTRAINT FK_Notificacion_Reserva FOREIGN KEY (IdReserva) REFERENCES Reserva(IdReserva)
);

CREATE TABLE EstadisticaDiaria (
    IdEstadistica   INT IDENTITY(1,1) PRIMARY KEY,
    Fecha           DATE NOT NULL,
    TotalReservas   INT NOT NULL DEFAULT 0,
    Confirmadas     INT NOT NULL DEFAULT 0,
    Canceladas      INT NOT NULL DEFAULT 0,
    Expiradas       INT NOT NULL DEFAULT 0,
    IngresosTotal   DECIMAL(18,2) NOT NULL DEFAULT 0,
    OcupacionPct    DECIMAL(5,2) NOT NULL DEFAULT 0,
    IdElemento      INT NOT NULL,
    CONSTRAINT FK_Estadistica_Elemento FOREIGN KEY (IdElemento) REFERENCES ElementoReservable(IdElemento)
);

CREATE TABLE DigitoVerificadorVertical (
    IdDVV           INT IDENTITY(1,1) PRIMARY KEY,
    NombreEntidad   VARCHAR(100) NOT NULL UNIQUE,
    ValorDVV        VARCHAR(500) NOT NULL
);

-- =============================================
-- DATOS DE EJEMPLO
-- =============================================

DECLARE @AdminId UNIQUEIDENTIFIER;
SELECT @AdminId = IdUsuario
FROM Reserva2_Servicio.dbo.Usuario
WHERE Email = 'admin@reserva2.com';

INSERT INTO Comercio (Nombre, Slug, Ubicacion, Activo, IdUsuario)
VALUES ('Complejo Deportivo Sur', 'complejo-deportivo-sur', 'Av. Rivadavia 4820', 1, @AdminId);

INSERT INTO Comercio (Nombre, Slug, Ubicacion, Activo, IdUsuario)
VALUES ('Padel Norte', 'padel-norte', 'Belgrano 1102', 1, @AdminId);

INSERT INTO ElementoReservable (Nombre, Descripcion, Tipo, PrecioBase, Estado, IdComercio)
VALUES ('Futbol 5 - Cancha A', 'Cesped sintetico, 10 jugadores', 'Futbol5', 18000, 'Activo', 1);

INSERT INTO ElementoReservable (Nombre, Descripcion, Tipo, PrecioBase, Estado, IdComercio)
VALUES ('Padel - Cancha 1', 'Cristal, 4 jugadores', 'Padel', 12500, 'Activo', 1);

INSERT INTO ElementoReservable (Nombre, Descripcion, Tipo, PrecioBase, Estado, IdComercio)
VALUES ('Padel - Cancha 2', 'Cristal, 4 jugadores', 'Padel', 12500, 'Activo', 1);

INSERT INTO ReglaDisponibilidad (DiaSemana, HoraInicio, HoraFin, DuracionSlot, Activo, IdElemento)
VALUES
('Lunes',     '18:00', '22:00', 60, 1, 1),
('Martes',    '18:00', '22:00', 60, 1, 1),
('Miercoles', '18:00', '22:00', 60, 1, 1),
('Jueves',    '18:00', '22:00', 60, 1, 1),
('Viernes',   '18:00', '22:00', 60, 1, 1),
('Sabado',    '10:00', '22:00', 60, 1, 1);

PRINT 'Base Reserva2_Negocio creada OK.';
GO

-- Restaurar modo multi-usuario (útil tras un restore/backup)
USE master;
GO
ALTER DATABASE Reserva2_Negocio SET MULTI_USER;
GO

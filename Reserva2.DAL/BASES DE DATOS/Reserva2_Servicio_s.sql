-- =============================================
-- RESERVA2 — Base de datos de SERVICIO
-- SQL Server 2019+
-- =============================================

CREATE DATABASE Reserva2_Servicio;
GO
USE Reserva2_Servicio;
GO

CREATE TABLE Usuario (
    IdUsuario       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    DNI             VARCHAR(500) NOT NULL,         -- VARCHAR(500): soporta DNI extranjeros/extensos
    Nombre          VARCHAR(100) NOT NULL,
    Email           VARCHAR(150) NOT NULL UNIQUE,
    PasswordHash    VARCHAR(500) NOT NULL,
    Rol             VARCHAR(50) NOT NULL,
    Telefono        VARCHAR(30),
    Estado          VARCHAR(20) NOT NULL DEFAULT 'Activo',
    FechaRegistro   DATETIME NOT NULL DEFAULT GETDATE(),
    DVH             VARCHAR(500)
);

CREATE TABLE Sesion (
    IdSesion        INT IDENTITY(1,1) PRIMARY KEY,
    Activa          BIT NOT NULL DEFAULT 1,
    FechaLogin      DATETIME NOT NULL DEFAULT GETDATE(),
    FechaLogout     DATETIME,
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Sesion_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE Permiso (
    IdPermiso       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nombre          VARCHAR(100) NOT NULL,
    Descripcion     VARCHAR(500),
    Tipo            VARCHAR(20) NOT NULL
);

CREATE TABLE PermisoSimple (
    IdPermiso       UNIQUEIDENTIFIER PRIMARY KEY,
    Clave           VARCHAR(100) NOT NULL UNIQUE,
    TipoAcceso      VARCHAR(30),
    CONSTRAINT FK_PermisoSimple_Permiso FOREIGN KEY (IdPermiso) REFERENCES Permiso(IdPermiso)
);

CREATE TABLE Rol (
    IdRol           UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nombre          VARCHAR(100) NOT NULL,
    Descripcion     VARCHAR(500)
);

CREATE TABLE RolPermiso (
    IdRol           UNIQUEIDENTIFIER NOT NULL,
    IdPermiso       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_RolPermiso PRIMARY KEY (IdRol, IdPermiso),
    CONSTRAINT FK_RolPermiso_Rol FOREIGN KEY (IdRol) REFERENCES Rol(IdRol),
    CONSTRAINT FK_RolPermiso_Permiso FOREIGN KEY (IdPermiso) REFERENCES Permiso(IdPermiso)
);

CREATE TABLE UsuarioRol (
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    IdRol           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_UsuarioRol PRIMARY KEY (IdUsuario, IdRol),
    CONSTRAINT FK_UsuarioRol_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_UsuarioRol_Rol FOREIGN KEY (IdRol) REFERENCES Rol(IdRol)
);

-- Composite: Familia (agrupador/rol del sistema)
CREATE TABLE Familia (
    IdFamilia       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Nombre          VARCHAR(100) NOT NULL
);

-- Composite: Patente (permiso atómico por pantalla)
CREATE TABLE Patente (
    IdPatente       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    DataKey         VARCHAR(100) NOT NULL,
    TipoAcceso      INT NOT NULL DEFAULT 0
);

-- Familia contiene Patentes
CREATE TABLE FamiliaPatente (
    IdFamilia       UNIQUEIDENTIFIER NOT NULL,
    IdPatente       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_FamiliaPatente PRIMARY KEY (IdFamilia, IdPatente),
    CONSTRAINT FK_FP_Familia FOREIGN KEY (IdFamilia) REFERENCES Familia(IdFamilia),
    CONSTRAINT FK_FP_Patente FOREIGN KEY (IdPatente) REFERENCES Patente(IdPatente)
);

-- Familia contiene otras Familias (relación recursiva)
CREATE TABLE FamiliaFamilia (
    IdFamiliaPadre  UNIQUEIDENTIFIER NOT NULL,
    IdFamiliaHijo   UNIQUEIDENTIFIER NOT NULL,       -- nombre final tras el sp_rename
    CONSTRAINT PK_FamiliaFamilia PRIMARY KEY (IdFamiliaPadre, IdFamiliaHijo),
    CONSTRAINT FK_FF_Padre FOREIGN KEY (IdFamiliaPadre) REFERENCES Familia(IdFamilia),
    CONSTRAINT FK_FF_Hijo  FOREIGN KEY (IdFamiliaHijo)  REFERENCES Familia(IdFamilia)
);

-- Usuario tiene Familias asignadas
CREATE TABLE UsuarioFamilia (
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    IdFamilia       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_UsuarioFamilia PRIMARY KEY (IdUsuario, IdFamilia),
    CONSTRAINT FK_UF_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_UF_Familia FOREIGN KEY (IdFamilia) REFERENCES Familia(IdFamilia)
);

-- Usuario tiene Patentes sueltas asignadas
CREATE TABLE UsuarioPatente (
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    IdPatente       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT PK_UsuarioPatente PRIMARY KEY (IdUsuario, IdPatente),
    CONSTRAINT FK_UP_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario),
    CONSTRAINT FK_UP_Patente FOREIGN KEY (IdPatente) REFERENCES Patente(IdPatente)
);

CREATE TABLE Bitacora (
    IdBitacora      INT IDENTITY(1,1) PRIMARY KEY,
    Accion          VARCHAR(100) NOT NULL,
    Entidad         VARCHAR(100),
    Detalle         VARCHAR(500),
    IdEntidad       INT,
    ValorAnterior   VARCHAR(MAX),
    ValorNuevo      VARCHAR(MAX),
    Origen          VARCHAR(100),
    IpOrigen        VARCHAR(50),
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario       UNIQUEIDENTIFIER,
    CONSTRAINT FK_Bitacora_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE LogError (
    IdError         INT IDENTITY(1,1) PRIMARY KEY,
    Mensaje         VARCHAR(MAX) NOT NULL,
    TipoExcepcion   VARCHAR(200),
    StackTrace      VARCHAR(MAX),
    Modulo          VARCHAR(100),
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario       UNIQUEIDENTIFIER,
    CONSTRAINT FK_LogError_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

CREATE TABLE DigitoVerificadorVertical (
    IdDVV           INT IDENTITY(1,1) PRIMARY KEY,
    NombreEntidad   VARCHAR(100) NOT NULL UNIQUE,
    ValorDVV        VARCHAR(500) NOT NULL
);

CREATE TABLE Notificacion (
    IdNotificacion  INT IDENTITY(1,1) PRIMARY KEY,
    Canal           VARCHAR(50) NOT NULL,
    Estado          VARCHAR(30) NOT NULL DEFAULT 'Pendiente',
    Tipo            VARCHAR(50),
    EnviadaEn       DATETIME,
    FechaCreacion   DATETIME NOT NULL DEFAULT GETDATE(),
    IdUsuario       UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_Notificacion_Usuario FOREIGN KEY (IdUsuario) REFERENCES Usuario(IdUsuario)
);

-- =============================================
-- DATOS INICIALES
-- =============================================

-- Usuario admin (PasswordHash = MD5 de 'admin123' en Unicode)
INSERT INTO Usuario (DNI, Nombre, Email, PasswordHash, Rol, Telefono, Estado)
VALUES ('00000000', 'Administrador', 'admin@reserva2.com',
        'c36725de6a71af4a302b55c0ab4fabcc', 'Admin', '', 'Activo');

-- Permisos (tabla legacy Permiso/PermisoSimple)
INSERT INTO Permiso (IdPermiso, Nombre, Descripcion, Tipo) VALUES
(NEWID(), 'frmComercios',       'Listar comercios',              'Patente'),
(NEWID(), 'frmComercioDetalle', 'Ver detalle de comercio',       'Patente'),
(NEWID(), 'frmUsuarios',        'Listar usuarios',               'Patente'),
(NEWID(), 'frmBitacora',        'Consultar bitacora',            'Patente'),
(NEWID(), 'frmErrores',         'Ver log de errores',            'Patente'),
(NEWID(), 'frmPermisos',        'Gestionar permisos',            'Patente'),
(NEWID(), 'frmElementos',       'Listar elementos reservables',  'Patente'),
(NEWID(), 'frmElementoABM',     'Alta/edicion de elementos',     'Patente'),
(NEWID(), 'frmDisponibilidad',  'Configurar disponibilidad',     'Patente'),
(NEWID(), 'frmCalendario',      'Ver calendario de slots',       'Patente'),
(NEWID(), 'frmReservas',        'Listar reservas',               'Patente'),
(NEWID(), 'frmReservaDetalle',  'Ver detalle de reserva',        'Patente'),
(NEWID(), 'frmNuevaReserva',    'Crear nueva reserva',           'Patente'),
(NEWID(), 'frmPagoManual',      'Registrar pago manual',         'Patente'),
(NEWID(), 'frmBackup',          'Gestionar backups',             'Patente'),
(NEWID(), 'frmIdioma',          'Cambiar idioma',                'Patente');

INSERT INTO PermisoSimple (IdPermiso, Clave, TipoAcceso)
SELECT IdPermiso, Nombre, 'Pantalla' FROM Permiso WHERE Tipo = 'Patente';

-- Patentes (Composite)
INSERT INTO Patente (DataKey, TipoAcceso) VALUES
('frmComercios',       0),
('frmComercioDetalle', 0),
('frmUsuarios',        0),
('frmPermisos',        0),
('frmBitacora',        0),
('frmErrores',         0),
('frmElementos',       0),
('frmElementoABM',     0),
('frmDisponibilidad',  0),
('frmCalendario',      0),
('frmReservas',        0),
('frmReservaDetalle',  0),
('frmNuevaReserva',    0),
('frmPagoManual',      0),
('frmBackup',          0),
('frmIdioma',          0);

-- Familia "Administrador" con todas las patentes
DECLARE @IdFamAdmin UNIQUEIDENTIFIER = NEWID();
INSERT INTO Familia (IdFamilia, Nombre) VALUES (@IdFamAdmin, 'Administrador');
INSERT INTO FamiliaPatente (IdFamilia, IdPatente)
SELECT @IdFamAdmin, IdPatente FROM Patente;

-- Familia "Comercio" con patentes de owner
DECLARE @IdFamCom UNIQUEIDENTIFIER = NEWID();
INSERT INTO Familia (IdFamilia, Nombre) VALUES (@IdFamCom, 'Comercio');
INSERT INTO FamiliaPatente (IdFamilia, IdPatente)
SELECT @IdFamCom, IdPatente FROM Patente
WHERE DataKey IN ('frmElementos','frmElementoABM','frmDisponibilidad','frmCalendario',
                  'frmReservas','frmReservaDetalle','frmNuevaReserva','frmPagoManual');

-- Familia "Cliente" con patentes de cliente
DECLARE @IdFamCli UNIQUEIDENTIFIER = NEWID();
INSERT INTO Familia (IdFamilia, Nombre) VALUES (@IdFamCli, 'Cliente');
INSERT INTO FamiliaPatente (IdFamilia, IdPatente)
SELECT @IdFamCli, IdPatente FROM Patente
WHERE DataKey IN ('frmNuevaReserva','frmReservas','frmReservaDetalle');

-- Asignar familia Administrador al usuario admin
INSERT INTO UsuarioFamilia (IdUsuario, IdFamilia)
SELECT u.IdUsuario, @IdFamAdmin
FROM Usuario u WHERE u.Email = 'admin@reserva2.com';

PRINT 'Base Reserva2_Servicio creada OK.';
GO

-- Restaurar modo multi-usuario (útil tras un restore/backup)
USE master;
GO
ALTER DATABASE Reserva2_Servicio SET MULTI_USER;
GO

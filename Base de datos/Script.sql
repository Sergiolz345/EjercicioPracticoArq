create database Clientes;

use Clientes;

CREATE TABLE Prospectos (
    idProspecto INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    primerApellido NVARCHAR(100) NOT NULL,
    segundoApellido NVARCHAR(100) NULL,
    calle NVARCHAR(100) NOT NULL,
    numero NVARCHAR(10) NOT NULL,
    colonia NVARCHAR(100) NOT NULL,
    codigoPostal NVARCHAR(10) NOT NULL,
    telefono NVARCHAR(15) NOT NULL,
    rfc NVARCHAR(13) NOT NULL,
    estatus NVARCHAR(20) NOT NULL CHECK (estatus IN ('Enviado', 'Autorizado', 'Rechazado')),
    fechaCreacion DATETIME DEFAULT GETDATE()
);


CREATE TABLE Documentos (
    idDocumento INT IDENTITY(1,1) PRIMARY KEY,
    idProspecto INT FOREIGN KEY REFERENCES Prospectos(idProspecto),
    nombreArchivo NVARCHAR(255) NOT NULL,
    tipoArchivo NVARCHAR(10) NOT NULL,
    rutaArchivo NVARCHAR(255) NOT NULL
);


CREATE TABLE Observaciones (
    idObservacion INT IDENTITY(1,1) PRIMARY KEY,
    idProspecto INT FOREIGN KEY REFERENCES Prospectos(idProspecto),
    observacion NVARCHAR(MAX) NOT NULL,
    fechaObservacion DATETIME DEFAULT GETDATE()
);


CREATE TABLE Usuarios (
    idUsuario INT IDENTITY(1,1) PRIMARY KEY,
    nombreUsuario NVARCHAR(50) NOT NULL,
    contraseña NVARCHAR(MAX) NOT NULL,
    rol NVARCHAR(20) NULL,
    imagenUsuario VARBINARY(MAX) NULL,
);


CREATE PROCEDURE sp_AgregarUsuario
    @nombreUsuario NVARCHAR(50),
    @contraseña NVARCHAR(MAX),
    @rol NVARCHAR(20) = NULL,
    @imagenUsuario VARBINARY(MAX) = NULL
AS
BEGIN
    INSERT INTO Usuarios (nombreUsuario, contraseña, rol, imagenUsuario)
    VALUES (@nombreUsuario, @contraseña, @rol, @imagenUsuario);
END;


CREATE PROCEDURE sp_VerificarUsuario
    @nombreUsuario NVARCHAR(50),
    @contraseña NVARCHAR(MAX)
AS
BEGIN
    SELECT * FROM Usuarios
    WHERE nombreUsuario = @nombreUsuario AND contraseña = @contraseña;
END;

CREATE TYPE DOCUMENTOS_TBLTYPE AS TABLE
(
    nombreArchivo NVARCHAR(255),
    tipoArchivo NVARCHAR(50),
    rutaArchivo NVARCHAR(500)
);


CREATE PROCEDURE sp_AgregarProspectoConDocumentos
    @nombre NVARCHAR(100),
    @primerApellido NVARCHAR(100),
    @segundoApellido NVARCHAR(100),
    @calle NVARCHAR(100),
    @numero NVARCHAR(10),
    @colonia NVARCHAR(100),
    @codigoPostal NVARCHAR(10),
    @telefono NVARCHAR(15),
    @rfc NVARCHAR(13),
    @estatus NVARCHAR(20),
    @documentos DOCUMENTOS_TBLTYPE READONLY
AS
BEGIN
    -- Iniciar una transacción para asegurar consistencia
    BEGIN TRANSACTION;

    -- Insertar el nuevo prospecto
    DECLARE @idProspecto INT;
    INSERT INTO Prospectos (nombre, primerApellido, segundoApellido, calle, numero, colonia, codigoPostal, telefono, rfc, estatus, fechaCreacion)
    VALUES (@nombre, @primerApellido, @segundoApellido, @calle, @numero, @colonia, @codigoPostal, @telefono, @rfc, @estatus, GETDATE());

    -- Obtener el id del nuevo prospecto
    SET @idProspecto = SCOPE_IDENTITY();

    -- Insertar los documentos asociados al prospecto
    INSERT INTO Documentos (idProspecto, nombreArchivo, tipoArchivo, rutaArchivo)
    SELECT @idProspecto, nombreArchivo, tipoArchivo, rutaArchivo FROM @documentos;

    -- Confirmar la transacción
    COMMIT TRANSACTION;
END;



CREATE PROCEDURE sp_ActualizarEstatusProspecto
    @nombreUseuario NVARCHAR(50),
    @nuevoEstatus NVARCHAR(20)
AS
BEGIN
    UPDATE Prospectos
    SET estatus = @nuevoEstatus
    WHERE nombre = @nombreUseuario;
END;


CREATE PROCEDURE sp_AgregarObservacionProspecto
    @idProspecto INT,
    @observacion NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Observaciones (idProspecto, observacion)
    VALUES (@idProspecto, @observacion);
END;


CREATE PROCEDURE sp_ObtenerDetallesProspectoPorNombreYApellidos
    @nombre NVARCHAR(100),
    @primerApellido NVARCHAR(100),
    @segundoApellido NVARCHAR(100)
AS
BEGIN
    -- Obtener los detalles del prospecto
    SELECT * FROM Prospectos
    WHERE nombre = @nombre AND primerApellido = @primerApellido AND segundoApellido = @segundoApellido;

    -- Obtener los documentos asociados al prospecto
    SELECT nombreArchivo, tipoArchivo, rutaArchivo FROM Documentos
    WHERE idProspecto = (SELECT idProspecto FROM Prospectos WHERE nombre = @nombre AND primerApellido = @primerApellido AND segundoApellido = @segundoApellido);

    -- Obtener las observaciones asociadas al prospecto
    SELECT observacion, fechaObservacion FROM Observaciones
    WHERE idProspecto = (SELECT idProspecto FROM Prospectos WHERE nombre = @nombre AND primerApellido = @primerApellido AND segundoApellido = @segundoApellido);
END;


CREATE PROCEDURE sp_ObtenerProspectosPorEstado
    @estado NVARCHAR(20)
AS
BEGIN
    SELECT * FROM Prospectos
    WHERE estatus = @estado;
END;



CREATE PROCEDURE sp_ObtenerDetallesProspectoRechazado
    @nombre NVARCHAR(100),
    @primerApellido NVARCHAR(100),
    @segundoApellido NVARCHAR(100)
AS
BEGIN
    -- Obtener los detalles del prospecto rechazado
    SELECT * FROM Prospectos
    WHERE nombre = @nombre AND primerApellido = @primerApellido AND segundoApellido = @segundoApellido AND estatus = 'Rechazado';

    -- Obtener los documentos asociados al prospecto rechazado
    SELECT nombreArchivo, tipoArchivo, rutaArchivo FROM Documentos
    WHERE idProspecto = (SELECT idProspecto FROM Prospectos WHERE nombre = @nombre AND primerApellido = @primerApellido AND segundoApellido = @segundoApellido);

    -- Obtener las observaciones asociadas al prospecto rechazado
    SELECT observacion, fechaObservacion FROM Observaciones
    WHERE idProspecto = (SELECT idProspecto FROM Prospectos WHERE nombre = @nombre AND primerApellido = @primerApellido AND segundoApellido = @segundoApellido);
END;

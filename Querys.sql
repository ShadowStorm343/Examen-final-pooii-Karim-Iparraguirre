CREATE DATABASE CibertecSeminarios
GO

USE CibertecSeminarios
GO

CREATE TABLE Seminario (
    CodigoSeminario INT NOT NULL,
    NombreCurso VARCHAR(100) NOT NULL,
    HorarioClase VARCHAR(50) NOT NULL,
    Capacidad INT NOT NULL,
    FotoUrl VARCHAR(200) NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    
    CONSTRAINT PK_Seminario PRIMARY KEY (CodigoSeminario),
    CONSTRAINT CK_Seminario_Capacidad CHECK (Capacidad >= 0)
)
GO

CREATE TABLE RegistroAsistencia (
    NumeroRegistro INT IDENTITY(1,1) NOT NULL,
    CodigoSeminario INT NOT NULL,
    CodigoEstudiante VARCHAR(20) NOT NULL,
    FechaRegistro DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_RegistroAsistencia PRIMARY KEY (CodigoSeminario, CodigoEstudiante),
    CONSTRAINT FK_RegistroAsistencia_Seminario 
        FOREIGN KEY (CodigoSeminario) REFERENCES Seminario(CodigoSeminario)
)
GO

CREATE PROCEDURE usp_ListarSeminariosDisponibles
AS
BEGIN
    SELECT 
        CodigoSeminario,
        NombreCurso,
        HorarioClase,
        Capacidad,
        FotoUrl
    FROM Seminario
    WHERE Capacidad > 0
END
GO

CREATE PROCEDURE usp_RegistrarAsistencia
    @CodigoSeminario INT,
    @CodigoEstudiante VARCHAR(20),
    @NumeroRegistro INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM Seminario 
        WHERE CodigoSeminario = @CodigoSeminario 
        AND Capacidad > 0
    )
    BEGIN
        INSERT INTO RegistroAsistencia (CodigoSeminario, CodigoEstudiante)
        VALUES (@CodigoSeminario, @CodigoEstudiante)

        SET @NumeroRegistro = SCOPE_IDENTITY()

        UPDATE Seminario
        SET Capacidad = Capacidad - 1
        WHERE CodigoSeminario = @CodigoSeminario
    END
    ELSE
    BEGIN
        SET @NumeroRegistro = -1
    END
END
GO

CREATE PROCEDURE usp_ObtenerSeminarioPorCodigo
    @CodigoSeminario INT
AS
BEGIN
    SELECT 
        CodigoSeminario,
        NombreCurso,
        HorarioClase,
        Capacidad,
        FotoUrl
    FROM Seminario
    WHERE CodigoSeminario = @CodigoSeminario
END
GO

INSERT INTO Seminario VALUES
(101, 'Introducción a C#', 'Lun-Mie 08:00-10:00', 30, 'csharp.jpg', DEFAULT),
(102, 'Fundamentos de SQL', 'Mar-Jue 10:00-12:00', 25, 'sql.jpg', DEFAULT),
(103, 'Programación Web', 'Vie 14:00-18:00', 20, 'web.jpg', DEFAULT),
(104, 'Python Básico', 'Sab 09:00-13:00', 15, 'python.jpg', DEFAULT),
(105, 'Java Orientado a Objetos', 'Lun-Mie 18:00-20:00', 10, 'java.jpg', DEFAULT),
(106, 'Introducción a Cloud', 'Dom 08:00-12:00', 5, 'cloud.jpg', DEFAULT)
GO

-- ====================================================================================
-- Pruebas rápidas
EXEC usp_ListarSeminariosDisponibles

DECLARE @NroRegistro INT
EXEC usp_RegistrarAsistencia 
    @CodigoSeminario = 101,
    @CodigoEstudiante = 'EST001',
    @NumeroRegistro = @NroRegistro OUTPUT

SELECT @NroRegistro AS NumeroRegistroGenerado




SELECT * FROM [dbo].[Seminario]

SELECT * FROM [dbo].[RegistroAsistencia]
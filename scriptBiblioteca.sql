  --DIEGO MADRID - SCRIPT PRUEBA BIBLIOTECA
CREATE DATABASE Biblioteca;
GO

USE Biblioteca;
GO

-- Crear la tabla Autores con GUID
CREATE TABLE Autores (
    AutorID UNIQUEIDENTIFIER   PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Nacionalidad NVARCHAR(50)
);
GO

-- Crear la tabla Libros con GUID
CREATE TABLE Libros (
    LibroID UNIQUEIDENTIFIER   PRIMARY KEY,
    Titulo NVARCHAR(200) NOT NULL,
    FechaPublicacion DATE,
    Genero NVARCHAR(50),
    ISBN CHAR(13) NOT NULL
);
GO

-- Crear la tabla intermedia LibrosAutores para la relación muchos a muchos con GUIDs
CREATE TABLE LibrosAutores (
    LibroAutorID UNIQUEIDENTIFIER PRIMARY KEY,
    LibroID UNIQUEIDENTIFIER NOT NULL,
    AutorID UNIQUEIDENTIFIER NOT NULL,
     FOREIGN KEY (LibroID) REFERENCES Libros(LibroID),
    FOREIGN KEY (AutorID) REFERENCES Autores(AutorID)
);
GO

-- Crear índices para mejorar el rendimiento de las búsquedas
CREATE INDEX IDX_Autores_Nombre ON Autores (Nombre);
CREATE INDEX IDX_Libros_Titulo ON Libros (Titulo);

GO
CREATE PROCEDURE ConsultarListaLibrosAutores
AS
BEGIN
     SELECT la.LibroAutorID, l.LibroID, a.AutorID, l.Titulo, (a.Nombre+' '+ a.Apellido) Autor
    FROM LibrosAutores la
	inner join Autores a on a.AutorID= la.AutorID
	inner join Libros l on l.LibroID= la.LibroID

END;

go

-- Insertar registros en la tabla Autores
DECLARE @AutorID1 UNIQUEIDENTIFIER = NEWID();
DECLARE @AutorID2 UNIQUEIDENTIFIER = NEWID();
DECLARE @AutorID3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Autores (AutorID, Nombre, Apellido, FechaNacimiento, Nacionalidad)
VALUES 
(@AutorID1, 'Gabriel', 'García Márquez', '1927-03-06', 'Colombiano'),
(@AutorID2, 'Isabel', 'Allende', '1942-08-02', 'Chilena'),
(@AutorID3, 'Julio', 'Cortázar', '1914-08-26', 'Argentino');
 

-- Insertar registros en la tabla Libros
DECLARE @LibroID1 UNIQUEIDENTIFIER = NEWID();
DECLARE @LibroID2 UNIQUEIDENTIFIER = NEWID();
DECLARE @LibroID3 UNIQUEIDENTIFIER = NEWID();

INSERT INTO Libros (LibroID, Titulo, FechaPublicacion, Genero, ISBN)
VALUES 
(@LibroID1, 'Cien Años de Soledad', '1967-05-30', 'Novela', '9780307474728'),
(@LibroID2, 'La Casa de los Espíritus', '1982-10-15', 'Novela', '9780553383805'),
(@LibroID3, 'Rayuela', '1963-06-28', 'Novela', '9788497935502');
 

-- Insertar registros en la tabla intermedia LibrosAutores
INSERT INTO LibrosAutores (LibroAutorID,LibroID, AutorID)
VALUES 
(newid(),@LibroID1, @AutorID1), -- Cien Años de Soledad - Gabriel García Márquez
(newid(),@LibroID2, @AutorID2), -- La Casa de los Espíritus - Isabel Allende
(newid(),@LibroID3, @AutorID3); -- Rayuela - Julio Cortázar
 
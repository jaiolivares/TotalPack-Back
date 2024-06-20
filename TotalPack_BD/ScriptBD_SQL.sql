USE master
GO

CREATE DATABASE [TestTotalPack]
GO

USE TestTotalPack
GO

CREATE TABLE Users
(
	Id UNIQUEIDENTIFIER PRIMARY KEY,-- DEFAULT NEWID(),
	FullName VARCHAR(50) NOT NULL DEFAULT '',
	Birth DATE NOT NULL,
	Email VARCHAR(100) NOT NULL DEFAULT ''
)
GO

DECLARE @GidJo UNIQUEIDENTIFIER = NEWID()
DECLARE @GidNd UNIQUEIDENTIFIER = NEWID()

INSERT INTO Users VALUES (@GidJo, 'Javier Olivares', '1987-10-14', 'javier.olivares.zavala@gmail.com')
INSERT INTO Users VALUES (@GidNd, 'Nicolas Droste', GETDATE(), 'ndroste@starthunt.cl')

CREATE TABLE Adresses
(
	IdAdress INT PRIMARY KEY IDENTITY(1,1),
	IdUser	UNIQUEIDENTIFIER NOT NULL,
	Street VARCHAR(100) NOT NULL DEFAULT '',
	Principal BIT NOT NULL DEFAULT 0
)

ALTER TABLE Adresses
ADD FOREIGN KEY (IdUser) REFERENCES Users (Id)

INSERT INTO Adresses VALUES (@GidJo, 'Pajaritos 1234', 0)
INSERT INTO Adresses VALUES (@GidJo, '5 de Abril 4321', 1)
INSERT INTO Adresses VALUES (@GidJo, 'Camino a Melipilla 999', 0)
INSERT INTO Adresses VALUES (@GidNd, 'Prueba 1111', 0)
INSERT INTO Adresses VALUES (@GidNd, 'Prueba 2222', 1)
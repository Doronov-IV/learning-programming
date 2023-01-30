/*  CREATE TABLES  */


USE QueryRepetition
GO
CREATE TABLE Budgets (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Value] FLOAT,
	[Units] NVARCHAR(24)
);




USE QueryRepetition
GO
CREATE TABLE Manufacturers (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24),
	[CountryName] NVARCHAR(24),
	[BudgetId] INT FOREIGN KEY REFERENCES Budgets (Id)
);




USE QueryRepetition
GO
CREATE TABLE Guns (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24) NOT NULL,
	[CaliberInMillimetres] INT NOT NULL,
	[LengthInCalibers] FLOAT NOT NULL,
	[ManufacturerId] INT FOREIGN KEY REFERENCES Manufacturers (Id)
);




USE QueryRepetition
GO
CREATE TABLE Engines (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24) NOT NULL,
	[HorsePowers] INT NOT NULL,
	[ManufacturerId] INT FOREIGN KEY REFERENCES Manufacturers (Id)
);




USE QueryRepetition
GO
CREATE TABLE Prices (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Value] FLOAT,
	[Currency] NVARCHAR(24),
	[Units] NVARCHAR(24)
);




USE QueryRepetition
GO
CREATE TABLE Tanks (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24),
	[ManufacturerId] INT FOREIGN KEY REFERENCES Manufacturers (Id),
	[GunId] INT FOREIGN KEY REFERENCES Guns (Id),
	[CrewCount] INT,
	[EngineId] INT FOREIGN KEY REFERENCES Engines (Id),
	[PriceId] INT FOREIGN KEY REFERENCES Prices (Id)
);

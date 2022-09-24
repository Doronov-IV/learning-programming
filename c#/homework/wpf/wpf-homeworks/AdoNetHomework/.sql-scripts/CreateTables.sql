USE DoronovAdoNetCoreHomework  

--Create the users table;
CREATE TABLE [Users]
(
   [Id] INT PRIMARY KEY IDENTITY(0,1),
   [Name] NVARCHAR(24) NOT NULL,
   [PhoneNumber] NVARCHAR(14)
)

--Create the orders table;
CREATE TABLE [Orders]
(
   [Id] INT PRIMARY KEY IDENTITY(0,1),
   [CustomerId] INT NOT NULL,
   [Sum] INT,
   [Date] DATE
)



Select *
From Users
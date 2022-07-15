USE StepDB
GO

/*
CREATE TABLE City (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24) NOT NULL,
);

CREATE TABLE Product (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24) NOT NULL,
	[Brand] NVARCHAR(24),
	[Quantity] INT,
	[Price] INT
);

CREATE TABLE Stock (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[CityId] INT FOREIGN KEY REFERENCES City (Id),
	[Address] NVARCHAR(24) NOT NULL
);

CREATE TABLE Accounting (
	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[StockId] INT FOREIGN KEY REFERENCES Stock (Id),
	[ProductId] INT FOREIGN KEY REFERENCES Product (Id)
);
*/




-- 21.05.2022


/*
INSERT INTO City VALUES (N'Los Angeles'),
(N'Philadelphia'),
(N'San Diego'),
(N'Portland'),
(N'Seatle'),
(N'Las Vegas');
*/


/*
INSERT INTO Product VALUES (N'Noodles', 'Mama Mia', '4200', 2),
(N'Noodles', 'Chidori Ramen', '200', 3),
(N'Noodles', 'Dochirak', '84200', 1),
(N'Noodles', 'Daewoo', '250', 3),
(N'Chocolate', 'Milka', '500', 2),
(N'Chocolate', 'Ritter Sport', '500', 4),
(N'Chocolate', 'Moser Roth', '400', 6),
(N'Chocolate', 'Schogetten', '100', 5),
(N'Chocolate', 'Kinder', '440', 4),
(N'Chocolate', 'Villars', '500', 6),
(N'Chocolate', 'Cailler', '800', 5),
(N'Chocolate', 'Toblerone', '500', 8);
*/


/*
	INSERT INTO Stock VALUES 
	(4, 'Wall Street, 122'),
	(3, 'Door Street, 101'),
	(1, 'Height Av, 14'),
	(6, 'Dostoevski, 1295'),
	(7, 'Fokewolf, 190'),
	(9, 'Strip idk, 1'),
	(8, 'Hello world, 9');
*/


/*
INSERT INTO Accounting VALUES 
(5,11),
(9,5),
(3,10),
(8,8),
(2,16),
(8,6),
(3,9),
(7,12);
*/


--/*
-- Show;
SELECT *
FROM City

SELECT *
FROM Stock

SELECT *
FROM Product

SELECT *
FROM Accounting
--*/




/*
-- Task One
/*
	--  "Get goods with AVG quantity <= some value";
*/


SELECT PRD.[Name], PRD.Brand, PRD.Quantity
FROM Product AS PRD
GROUP BY PRD.[Name], PRD.Brand, PRD.Quantity
HAVING AVG(PRD.Quantity) <= 2000
ORDER BY PRD.Quantity

*/


--/*
-- Nested single-value Query Demo;
SELECT PRD.Name AS Product, PRD.Price, PRD.Quantity
FROM Product AS PRD
WHERE PRD.Price = (SELECT MAX(PRD.Price) FROM Product AS PRD)
GROUP BY PRD.Name, PRD.Price, PRD.Quantity
--*/ 


--/*
-- Nested single-value Query Demo;
SELECT PRD.Brand
FROM Product AS PRD
WHERE PRD.Id IN (

SELECT Id
FROM Product AS PRD
WHERE PRD.Price < 5)
--*/ 



/*
-- Task Two
/*
	--  "Get goods with AVG quantity <= some value";
*/

SELECT PRD.[Name], PRD.Brand, PRD.Quantity
FROM Product AS PRD
GROUP BY PRD.[Name], PRD.Brand, PRD.Quantity
HAVING AVG(PRD.Quantity) <= 2000
ORDER BY PRD.Quantity

*/



/*
-- Task Three
/*
	--  "Get the amount of brands all some goods of some city";
*/

SELECT COUNT(*)
FROM
(SELECT Brand
FROM Product AS PRD, Stock AS STK, City AS CIT, Accounting AS ACC
WHERE PRD.Id = ACC.ProductId AND STK.Id = ACC.StockId AND CIT.Id = STK.CityId AND CIT.Name = 'Chicago' AND
PRD.Name IN ('Corn Crisps', 'Potato Crisps')) AS M

*/



/*
-- "Get the Names of the cities where some specific product is stored";
SELECT CIT.Name
FROM City AS CIT
WHERE CIT.Id IN (
SELECT STK.CityId
FROM Stock AS STK
WHERE Id IN ( 
SELECT StockId FROM Accounting AS ACC 
WHERE ProductId IN ( 
SELECT Id FROM Product AS PRD WHERE PRD.Name = 'Noodles' )))
*/


--/*
-- Task Four
/*
	--  "Get the stocks where some group of goods is stored less then some value in a specific city";
*/

-- Garbage;
SELECT STK.Address
FROM Stock AS STK
WHERE STK.Id IN (
SELECT PRD.Id
FROM Product AS PRD
WHERE PRD.Name IN ('Corn Crisps','Potato Crisps') AND PRD.Quantity < 1000 AND PRD.Id = (
SELECT ACC.ProductId 
FROM Accounting AS ACC
WHERE Id = (
SELECT CIT.Id
FROM City AS CIT
WHERE CIT.Name = 'Atlanta')))


-- More like Truth;
SELECT STK.Address FROM Stock AS STK WHERE STK.CityId = 
(SELECT Id FROM City AS CIT WHERE CIT.Name = 'Chicago') 
AND STK.Id = 
(SELECT ACC.StockId FROM Accounting AS ACC WHERE ACC.StockId = 
(SELECT CIT.Id FROM City AS CIT))


-- Teacher's
SELECT STK.Address
FROM Stock AS STK
WHERE STK.Id IN (

SELECT ACC.StockId
FROM Accounting AS ACC
WHERE ACC.ProductId IN (

SELECT PRD.Id
FROM Product AS PRD
WHERE PRD.Name IN ('Corn Crisps','Potato Crisps') AND PRD.Quantity < 1000)) AND CityId IN (

SELECT Id
FROM City AS CIT
WHERE CIT.Name = 'Chicago')

--*/


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


/*
-- AS keyword redefinition
SELECT [Name], [Address]
FROM City AS C, Stock AS S

SELECT [Name], [Address]
FROM City AS C, Stock AS S
WHERE C.Id = S.CityId
*/

/*
-- repetative info filter
SELECT Address, [Name]
FROM Stock AS STK, Product AS PRD, Accounting AS ACC
WHERE STK.Id = ACC.StockId AND PRD.Id = ACC.ProductId
*/

/*
-- DISTINCT keyword
SELECT City.[Name], [Brand]
FROM City, Stock, Product, Accounting
WHERE City.Id = Stock.CityId AND Stock.Id = Accounting.StockId AND Product.Id = Accounting.ProductId

SELECT DISTINCT City.[Name], [Brand]
FROM City, Stock, Product, Accounting
WHERE City.Id = Stock.CityId AND Stock.Id = Accounting.StockId AND Product.Id = Accounting.ProductId
*/





-- 20.05.2022



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
-- Task one. Quick Revise;

/*
	Count quantity of goods of a certain category
	and price < *some price*
*/

SELECT COUNT (*) AS Result
FROM Product AS PRD
WHERE PRD.Price > 2 AND PRD.Name IN( 'Potato Crisps','Corn Crisps');

*/


/*
-- Task two MIN Function;

/*
	Find out the least quantity of goods we have;
*/

SELECT MIN(PRD.Quantity) AS MinQuantity
FROM Product AS PRD

*/


/*
-- Task three. AVG Function;

/*
	Get the AVG price of some specific good
	located in a specific city (modified for many cities to check);
*/

SELECT AVG(PRD.Price) AS AveragePrice
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE ACC.ProductId = PRD.Id AND PRD.Name = 'Corn Crisps' AND ACC.StockId = STK.Id AND STK.CityId = CIT.Id AND CIT.Name = 'Arisona'
OR CIT.Name = 'New York' OR CIT.Name = 'Chicago'

*/



/*
-- Task four. GROUP BY Keyword [1];

/*
	Group stocks with their goods by address;
*/

SELECT CIT.Name + ' ' + STK.Address, SUM(PRD.Quantity) AS 'Product Quantity'
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE PRD.Id = ACC.ProductId AND STK.Id = ACC.StockId AND CIT.Id = STK.CityId
GROUP BY CIT.Name + ' ' + STK.Address

*/



/*
-- Task five. GROUP BY Keyword [2];

/*
	Select stocks that got some specific good and the quantity of that good;
*/

SELECT CIT.Name + ' ' + STK.Address AS Stock, SUM(PRD.Quantity) AS 'Product Quantity'
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE PRD.Id = ACC.ProductId AND STK.Id = ACC.StockId AND CIT.Id = STK.CityId AND PRD.Name = 'Corn Crisps'
GROUP BY CIT.Name + ' ' + STK.Address

-- + <<PRD.Name = 'Potato Crisps'>>

*/


---
--- Second Class;
---


--/*
-- Task six. GROUP BY Keyword [3];

/*
	Get amount of stocks in some city where some 
	specific good is present;
*/

SELECT CIT.Name AS City, COUNT(STK.Id) AS 'Stock Quantity'
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE PRD.Id = ACC.ProductId AND STK.Id = ACC.StockId AND CIT.Id = STK.CityId AND PRD.Brand = 'Estrella'
GROUP BY CIT.Name

--*/



--/*
-- Task six. GROUP BY Keyword [4];

/*
	1) Amount of Brands of some Group of Goods that are stored in a specific City;
	2) Overall Amount of all Goods of all Brands;
*/

-- 1) Teacher: "This Task is incorrect. We haven't learned nested queries yet. Nevermind it."  
-- UPD. Teacher: "DISTINCT Keyword doesn't solve this Problem despite filtering some Stuff."
SELECT PRD.Brand AS Brand, COUNT(PRD.Brand) AS 'Brands Quantity'
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE PRD.Id = ACC.ProductId AND STK.Id = ACC.StockId AND CIT.Id = STK.CityId AND CIT.Name = 'Chicago' AND 
PRD.Name IN ('Corn Crisps','Potato Crisps')
GROUP BY PRD.Brand

-- 2)
SELECT PRD.Brand AS Brand, SUM(PRD.Quantity) AS 'Goods Quantity'
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE PRD.Id = ACC.ProductId AND STK.Id = ACC.StockId AND CIT.Id = STK.CityId
GROUP BY PRD.Brand

--*/
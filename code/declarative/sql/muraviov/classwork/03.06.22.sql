USE StepDB
GO

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
-- Task One;

--/*
	-- Quantity of all goods of all stocks in a specific city;
--*/

-- Multiple conditions;

SELECT SUM(PRD.Quantity) AS Sum, STK.Address
FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
WHERE CIT.Name = 'New York' AND ACC.ProductId = PRD.Id AND ACC.StockId = STK.Id AND STK.CityId = CIT.Id
GROUP BY STK.Address

-- Nested Queries;

-- 1)

SELECT STK.Address 
FROM Stock AS STK
WHERE STK.Id IN (

SELECT ACC.StockId
FROM Accounting AS ACC
WHERE ACC.ProductId IN (

SELECT PRD.Id
FROM Product AS PRD
WHERE  PRD.Quantity < 1000)) AND CityId IN (

SELECT Id
FROM City AS CIT
WHERE CIT.Name = 'New York')


-- 2)

SELECT COUNT(PRD.Name)
FROM Product AS PRD
WHERE PRD.Id IN (
	SELECT ACC.ProductId
	FROM Accounting AS ACC
	WHERE ACC.StockId IN (
		SELECT Id  
		FROM Stock AS STK
		WHERE CityId IN (
			SELECT Id
			FROM City AS CIT
			WHERE CIT.Name IN ('New York')
			)
		)
	)

*/
		


/*
-- Task Two;

--/*
	-- Get the name of the city where the highest amount of different goods is stored;
--*/


SELECT COUNT(PRD.Name) AS Overall_Names_Quantity
FROM Product AS PRD
WHERE PRD.Id IN (
	SELECT ACC.ProductId
	FROM Accounting AS ACC
	WHERE ACC.StockId IN (
		SELECT Id  
		FROM Stock AS STK
		WHERE CityId IN (
			SELECT Id
			FROM City AS CIT
			)
		)
	)


SELECT CIT.Name
FROM (
	SELECT MAX(Names_Quantity)
	FROM (
		SELECT CIT.Name AS [Name], COUNT(PRD.Id) AS Names_Quantity
		FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
		WHERE CIT.Id = STK.CityId AND STK.Id = ACC.StockId AND PRD.Id = ACC.ProductId
		GROUP BY CIT.Name
	) AS CityProduct
)

*/


--/*
-- Task Three;

--/*
	-- Get the most quantity of stocks from all cities (or something, idk);
--*/

-- Works correct, if I got it right;
SELECT MAX(Quantity) AS Max_Stock_Quantity
FROM (
	SELECT CIT.Name, COUNT(CIT.Id) AS Quantity
	FROM City AS CIT, Product AS PRD, Accounting AS ACC, Stock AS STK
	WHERE ACC.StockId = STK.Id AND STK.CityId = CIT.Id AND PRD.Id = ACC.ProductId
	GROUP BY CIT.Name)
AS Quantity


--*/
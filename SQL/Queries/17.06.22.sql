
USE StepDB
GO

--/*--
-- Show product tables;
SELECT *
FROM City

SELECT *
FROM Stock

SELECT *
FROM Product

SELECT *
FROM Accounting
--*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--
-- Task One ('UNION' keyword revise);
/*
	-- Select teachers with their age (from 20 to 50 y.o.) and students with their age with UNION;

	-- Select all cities with address and id and stocks with same stuff but id < 5 (idk, I don't have students table);
*/


-- 1) My Query

SELECT CIT.Id, CIT.Name
FROM City AS CIT
WHERE CIT.Id IN (SELECT CIT.Id
				 FROM City AS CIT
				 WHERE CIT.Id < 5)


UNION ALL


SELECT STK.Id, STK.Address
FROM Stock AS STK
ORDER BY Name ASC


-- 2) Teacher's


SELECT * 
FROM
	(SELECT CIT.Id, CIT.Name
	FROM City AS CIT
	UNION ALL
	SELECT STK.Id, STK.Address
	FROM Stock AS STK) AS People 
WHERE Id < 5


--/*

-- Teacher's demo;

SELECT 'Cities' AS Type, COUNT(*) AS Count
FROM City 

UNION ALL

SELECT 'StockS' AS Type, COUNT(*) AS Count
FROM Stock 

--*/

*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--
-- Task Two ('UNION' keyword revise);
/*
	Teacher:

				Students | Quantity
				---------|---------
				Young	 |	  3
				Middle	 |	  2
				 Old	 |	  6

	Me: (Down below)
*/


/*-- nvm, it's stupid, but I'll keep it for some reason;

SELECT 'North' AS Type, COUNT(*) AS Count
FROM City AS CIT
WHERE CIT.Name IN (SELECT CIT.Name 
				   FROM City AS CIT
				   WHERE CIT.Name = 'New York' OR CIT.Name = 'Chicago')
UNION ALL
SELECT 'Centre', COUNT(*) 
FROM City AS CIT
WHERE CIT.Name IN (SELECT CIT.Name 
				   FROM City AS CIT
				   WHERE CIT.Name = 'Las Vegas' OR CIT.Name = 'Portland' OR CIT.Name = 'Seatle' )
UNION ALL
SELECT 'South', COUNT(*) 
FROM City AS CIT
WHERE CIT.Name IN (SELECT CIT.Name 
				   FROM City AS CIT
				   WHERE CIT.Name = 'San Diego' OR CIT.Name = 'Atlanta' OR CIT.Name = 'Arisona' OR CIT.Name = 'Los Angeles')

*/


----------------------------------------------------------


SELECT 'Expensive' AS Type, COUNT(*) AS Count
FROM Product AS PRD
WHERE PRD.Price > 5

UNION ALL

SELECT 'Medium' AS Type, COUNT(*) AS Count
FROM Product AS PRD
WHERE PRD.Price > 3 AND PRD.Price < 5

UNION ALL

SELECT 'Cheap' AS Type, COUNT(*) AS Count
FROM Product AS PRD
WHERE PRD.Price < 3


*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--
-- Task Three ('JOIN' keyword);
/*
	-- join city and stock address on city;
*/



SELECT City.Name, Stock.Address
FROM City INNER JOIN Stock
ON City.Id = Stock.CityId
ORDER BY City.Name ASC




*/--


------------------------------------------------------------------------------------------------------------------------------------------


--/*--
-- Task Four ('JOIN' keyword);
/*
	-- Join product name with their stock address;
*/



SELECT Product.Brand, Product.Name, Stock.Address
FROM Stock INNER JOIN Accounting
ON Stock.Id = Accounting.StockId JOIN Product
									  ON Accounting.ProductId = Product.Id 
									  ORDER BY Stock.Address ASC



--*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--
-- Task Five ('JOIN' keyword);
/*
	-- Join product name with their stock address + city + some id;
*/



SELECT Product.Brand, Product.Name, Stock.Address, City.Name
--
FROM Stock INNER JOIN City 
		   ON City.Id = Stock.CityId INNER JOIN Accounting
									 ON Stock.Id = Accounting.StockId INNER JOIN Product
																	  ON Accounting.ProductId = Product.Id 
--
ORDER BY City.Name ASC


*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--
-- Task Six ('JOIN' keyword);
/*
	-- Add Stocks w/o city and select them via JOIN;
*/

/*
INSERT INTO Stock(Address) VALUES 
(N'Hellfire st, 66'),
(N'Barbarossa st, 9')
*/


-- 1) My query;

SELECT City.Name, Stock.Address
FROM City RIGHT JOIN Stock
		  ON City.Id = Stock.CityId
		  WHERE Stock.CityId IS NULL
		  ORDER BY City.Name ASC


-- 2) Teacher's proposal (I think I misunderstood him);

/*
SELECT City.Name, Stock.Address

FROM Stock INNER JOIN City
		  ON City.Id = Stock.CityId
		  WHERE Stock.CityId IS NULL

ORDER BY City.Name ASC
*/

*/--


------------------------------------------------------------------------------------------------------------------------------------------


--/*--
-- Task Seven ('JOIN' keyword);
/*
	-- Add cities w/o stocks;
	-- Then ....
*/

/*
	-- Me: Already got 1 or 2;
*/


-- City Left join Store
SELECT City.Name, Stock.Address
--
FROM City LEFT JOIN Stock
		  ON City.Id = Stock.CityId
--
ORDER BY City.Name ASC


-- City Right join Store
SELECT City.Name, Stock.Address
--
FROM City RIGHT JOIN Stock
		  ON City.Id = Stock.CityId
--
ORDER BY City.Name ASC


-- City Full join Store
SELECT City.Name, Stock.Address
--
FROM City FULL JOIN Stock
		  ON City.Id = Stock.CityId
--
ORDER BY City.Name ASC



--*/--


------------------------------------------------------------------------------------------------------------------------------------------
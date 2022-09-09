USE StepDB
GO

/*--
-- Show product tables;

SELECT *
FROM City

SELECT *
FROM Stock

SELECT *
FROM Product

SELECT *
FROM Accounting
*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--

-- Task One ('EXISTS' keyword);

/*
	-- Add some goods that would not be added to ACC and select them with EXISTS instruction;
*/


/*INSERT INTO Product VALUES (N'Domestos', 'Pepsico', '220', '5'),
(N'Pemolux', 'Pepsico', '222', '4'),
(N'SomeName', 'idk_TM', '600', '3'),
(N'Wonderflavore', 'Baba Nura', '2200', '2'),
(N'Persault', 'Homeless Petrovich', '9999', '1');*/ --Done;


SELECT *
FROM Product AS PRD
WHERE NOT EXISTS (SELECT * 
				  FROM Accounting AS ACC
				  WHERE ACC.ProductId = PRD.Id)


*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--

-- Task Two ('EXISTS' keyword);

/*
	---- Add some empty stocks and select only those which contain goods;
*/


/*INSERT INTO Stock VALUES 
(1,'Length Street, 45'),
(2,'Your Dad Street, 22'),
(3,'Floor Av, 9'),
(4,'Idk Street, 69'),
(5,'Pushkeen Square, 1');*/ -- Done;


SELECT *
FROM Stock AS STK
WHERE NOT EXISTS (SELECT * 
				  FROM Accounting AS ACC
				  WHERE ACC.StockId= STK.Id)


*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--

-- Task Three ('ANY/SOME' keywords);

/*
	-- Do some stuff with those keywords;
	-- Me: It gets avg price of all goods and selects those which have higher price;
*/


SELECT *
FROM Product AS PRD
WHERE PRD.Brand = ANY (SELECT PRD.Brand 
						FROM Product AS PRD
						WHERE PRD.Price = (SELECT AVG(PRD.Price)
										   FROM Product AS PRD))

*/--


------------------------------------------------------------------------------------------------------------------------------------------


/*--

-- Task Four ('ALTER TABLE');

/*
	-- Its goals are not clearly specified so I decided to skip it;
	-- Besides this, the teacher said to use the 'Students' table which is somewhat deleted from my db;
	
	-- Anyway, I seem to get this topic since recent time;
*/


-- write your code here ...

*/--


------------------------------------------------------------------------------------------------------------------------------------------


--/*--

-- Task Five ('UNION' keyword);

/*
	-- Demo task;
	-- Me: idk what that crap does;
*/


SELECT PRD.Price AS SomeFancyName
FROM Product AS PRD
UNION --ALL
SELECT ACC.Id
FROM Accounting AS ACC
ORDER BY PRD.Price DESC

--*/--

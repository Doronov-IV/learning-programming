USE QueryRepetitionEntity
GO

CREATE PROCEDURE select_autoloaders
AS
BEGIN

SELECT *
FROM Tanks AS TNK
WHERE TNK.CrewCount = 3

END



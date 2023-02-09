USE QueryRepetitionEntity 
GO

CREATE PROCEDURE insert_budget
(@Value BIGINT,
@Currency NVARCHAR(3),
@BudgetId INT OUTPUT)
AS
BEGIN

INSERT INTO Budgets ([Value], [Currency]) VALUES (@Value, @Currency)

SELECT @BudgetId = SCOPE_IDENTITY()

END
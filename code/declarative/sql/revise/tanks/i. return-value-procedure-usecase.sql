USE QueryRepetitionEntity
GO

DECLARE @BudgetId INT

EXEC insert_budget '50000', '$', @BudgetId OUTPUT

SELECT *
FROM Budgets
WHERE Budgets.Id = @BudgetId
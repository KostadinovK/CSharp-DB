CREATE PROC usp_CalculateFutureValueForAccount(@accountId INT, @yearlyInterestRate FLOAT) AS
BEGIN
	SELECT
		a.Id AS [Account Id],
		ah.FirstName AS [First Name],
		ah.LastName AS [Last Name], 
		a.Balance AS [Current Balance],
		dbo.ufn_CalculateFutureValue(a.Balance, @yearlyInterestRate, 5) AS [Balance in 5 years]
	FROM AccountHolders ah
	JOIN Accounts a ON ah.Id = a.AccountHolderId
	WHERE a.Id = @accountId
END;
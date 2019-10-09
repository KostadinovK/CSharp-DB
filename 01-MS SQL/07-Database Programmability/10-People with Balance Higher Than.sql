CREATE PROC usp_GetHoldersWithBalancehigherThan(@balance DECIMAL(18,2)) AS
BEGIN
	SELECT 
		ah.FirstName, ah.LastName
	FROM AccountHolders ah
	JOIN Accounts a ON ah.Id = a.AccountHolderId
	GROUP BY a.AccountHolderId, ah.FirstName, ah.LastName
	HAVING SUM(a.Balance) > @balance
	ORDER BY ah.FirstName, ah.LastName 
END;
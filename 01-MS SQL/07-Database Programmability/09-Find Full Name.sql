CREATE PROC usp_GetHoldersFullName AS
BEGIN
	SELECT
		CONCAT(a.FirstName, ' ', a.LastName) AS [Full Name]
	FROM AccountHolders a
END;
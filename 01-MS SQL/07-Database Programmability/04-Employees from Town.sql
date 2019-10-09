CREATE PROC usp_GetEmployeesFromTown(@town VARCHAR(50)) AS
BEGIN
	SELECT
		e.FirstName, e.LastName
	FROM Employees e
	JOIN Addresses a ON e.AddressID = a.AddressID
	JOIN Towns t ON a.TownID = t.TownID
	WHERE t.[Name] = @town;
END;
CREATE PROC usp_GetEmployeesSalaryAboveNumber(@minSalary DECIMAL(18, 4)) AS
BEGIN
	SELECT
		e.FirstName, e.LastName
	FROM Employees e
	WHERE e.Salary >= @minSalary;
END;
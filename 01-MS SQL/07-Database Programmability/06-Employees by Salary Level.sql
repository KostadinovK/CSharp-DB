CREATE PROC usp_EmployeesBySalaryLevel(@salaryLevel VARCHAR(7)) AS
BEGIN
	SELECT
		e.FirstName, e.LastName
	FROM Employees e
	WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @salaryLevel;
END;
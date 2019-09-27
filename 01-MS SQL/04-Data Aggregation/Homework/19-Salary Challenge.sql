SELECT TOP(10)
	FirstName, LastName, Result.DepartmentID
FROM Employees,
	(SELECT
		DepartmentID,
		AVG(Salary) AS AverageSalary
	FROM Employees
	GROUP BY DepartmentID)
	AS Result
WHERE Result.DepartmentID = Employees.DepartmentID
	AND Employees.Salary > Result.AverageSalary
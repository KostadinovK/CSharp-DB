SELECT DISTINCT
	DepartmentID,
	Salary
FROM 
(
	SELECT
		DepartmentID,
		Salary,
		DENSE_RANK() OVER ( PARTITION BY DepartmentID ORDER BY Salary DESC) AS SalaryNumber
	FROM Employees
) AS Result
WHERE Result.SalaryNumber = 3
ORDER BY Result.DepartmentID

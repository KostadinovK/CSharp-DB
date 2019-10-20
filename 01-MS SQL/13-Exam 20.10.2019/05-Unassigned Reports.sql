SELECT
	[Description], 
	FORMAT(OpenDate, 'dd-MM-yyyy') AS OpenDate 
FROM Reports
WHERE EmployeeId IS NULL
ORDER BY FORMAT(OpenDate, 'yyyy-MM-dd'), [Description];
SELECT
	ISNULL(e.FirstName + ' ' + e.LastName, 'None') AS Employee,
	ISNULL(d.[Name], 'None') AS Department,
	c.[Name] AS Category,
	r.[Description],
	FORMAT(r.OpenDate, 'dd.MM.yyyy') AS OpenDate,
	s.Label AS [Status],
	u.[Name] AS [User]
FROM Reports r
LEFT JOIN Employees e ON e.Id = r.EmployeeId
LEFT JOIN Departments d ON d.Id = e.DepartmentId
LEFT JOIN Categories c ON c.Id = r.CategoryId
LEFT JOIN [Status] s ON s.Id = r.StatusId
LEFT JOIN Users u ON u.Id = r.UserId
ORDER BY 
e.FirstName DESC,
e.LastName DESC,
Department,
Category,
r.[Description],
OpenDate,
[Status],
[User];
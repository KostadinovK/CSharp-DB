SELECT
	e.FirstName + ' ' + e.LastName AS FullName,
	COUNT(r.UserId) AS UsersCount
FROM Reports r
RIGHT JOIN Employees e ON r.EmployeeId = e.Id
GROUP BY e.FirstName + ' ' + e.LastName
ORDER BY UsersCount DESC, FullName;
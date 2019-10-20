SELECT
	u.Username, c.[Name]
FROM Users u
JOIN Reports r ON u.Id = r.UserId
JOIN Categories c ON c.Id = r.CategoryId
WHERE FORMAT(r.OpenDate, 'dd-MM') = FORMAT(u.Birthdate, 'dd-MM')
ORDER BY u.Username, c.[Name];
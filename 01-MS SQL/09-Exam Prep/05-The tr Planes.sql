SELECT
	*
FROM Planes
WHERE CHARINDEX('tr', [Name]) > 0
ORDER BY Id, [Name], Seats, [Range];
SELECT
	Id, [Name], Size
FROM Files
WHERE Size > 1000 AND CHARINDEX('html', [Name]) > 0
ORDER BY Size DESC, Id, [Name];
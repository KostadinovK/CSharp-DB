SELECT
	p.[Name], COUNT(s.[Name]) AS [Count]
FROM Planets p
LEFT JOIN Spaceports s ON s.PlanetId = p.Id
GROUP BY p.[Name]
ORDER BY [Count] DESC, p.[Name];
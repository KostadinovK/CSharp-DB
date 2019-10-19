SELECT
	p.[Name], COUNT(*) AS JourneysCount
FROM Planets p
JOIN Spaceports s ON s.PlanetId = p.Id
JOIN Journeys j ON j.DestinationSpaceportId = s.Id
GROUP BY p.[Name]
ORDER BY COUNT(*) DESC, p.[Name];
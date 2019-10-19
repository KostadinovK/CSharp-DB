SELECT TOP(1)
	j.Id, p.[Name] AS PlanetName, s.[Name] AS SpaceportName, j.Purpose AS JourneyPurpose
FROM Journeys j
JOIN Spaceports s ON j.DestinationSpaceportId = s.Id
JOIN Planets p ON s.PlanetId = p.Id
ORDER BY DATEDIFF(HOUR, JourneyStart, JourneyEnd);
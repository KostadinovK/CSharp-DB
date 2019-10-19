SELECT TOP(1)
	ships.[Name],
	[ports].[Name] 
FROM Spaceships ships
JOIN Journeys j ON j.SpaceshipId = ships.Id
JOIN Spaceports [ports] ON j.DestinationSpaceportId = [ports].Id
ORDER BY LightSpeedRate DESC;
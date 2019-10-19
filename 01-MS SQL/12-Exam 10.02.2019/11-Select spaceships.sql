SELECT
	s.[Name], s.Manufacturer 
FROM Spaceships s
JOIN Journeys j ON j.SpaceshipId = s.Id
JOIN TravelCards t ON t.JourneyId = j.Id
JOIN Colonists c ON t.ColonistId = c.Id
WHERE DATEDIFF(YEAR, c.BirthDate, '2019/01/01') < 30 AND t.JobDuringJourney = 'Pilot'
ORDER BY s.[Name];

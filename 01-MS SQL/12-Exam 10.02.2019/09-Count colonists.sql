SELECT
	COUNT(*) AS [count]
FROM Colonists c
JOIN TravelCards t ON c.Id = t.ColonistId
JOIN Journeys j ON t.JourneyId = j.Id
WHERE j.Purpose = 'Technical';
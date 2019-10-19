SELECT
	c.Id,
	c.FirstName + ' ' + c.LastName AS full_name
FROM Colonists c
JOIN TravelCards t ON c.Id = t.ColonistId
WHERE t.JobDuringJourney = 'Pilot'
ORDER BY Id, full_name;
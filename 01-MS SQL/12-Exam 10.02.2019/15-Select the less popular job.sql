SELECT TOP(1)
	t.JourneyId,
	t.JobDuringJourney AS JobName
FROM TravelCards t
WHERE t.JourneyId IN( SELECT TOP(1) Id FROM Journeys ORDER BY DATEDIFF(HOUR, JourneyStart, JourneyEnd) DESC)
GROUP BY t.JobDuringJourney, t.JourneyId
ORDER BY COUNT(*);

SELECT k.JobDuringJourney, c.FirstName + ' ' + c.LastName AS FullName, k.JobRank
FROM 
(
	SELECT tc.JobDuringJourney AS JobDuringJourney, tc.ColonistId,
		DENSE_RANK() OVER (PARTITION BY tc.JobDuringJourney ORDER BY co.Birthdate ASC) AS JobRank
	FROM TravelCards AS tc
	JOIN Colonists AS co ON co.Id = tc.ColonistId
	GROUP BY tc.JobDuringJourney, co.Birthdate, tc.ColonistId
) AS k
JOIN Colonists AS c ON c.Id = k.ColonistId
WHERE k.JobRank = 2
ORDER BY k.JobDuringJourney
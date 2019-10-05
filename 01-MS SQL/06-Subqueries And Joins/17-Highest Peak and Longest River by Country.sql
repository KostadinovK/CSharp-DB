SELECT TOP(5)
	CountryName, 
	Elevation AS HighestPeakElevation,
	[Length] AS LongestRiverLength 
FROM
(
	SELECT 
		c.CountryName,
		p.Elevation,
		r.[Length],
		DENSE_RANK() OVER(PARTITION BY CountryName ORDER BY p.Elevation DESC) AS HighestPeak,
		DENSE_RANK() OVER(PARTITION BY CountryName ORDER BY r.[Length] DESC) AS LongestRiver
	FROM Countries c
	LEFT JOIN MountainsCountries mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains m ON mc.MountainId = m.Id
	LEFT JOIN Peaks p ON m.Id = p.MountainId
	LEFT JOIN CountriesRivers cr ON c.CountryCode = cr.CountryCode
	LEFT JOIN Rivers r ON cr.RiverId = r.Id
	GROUP BY c.CountryName, p.Elevation, r.[Length]
	HAVING p.Elevation > 0 AND r.[Length] > 0
) AS Result
WHERE HighestPeak = 1 AND LongestRiver = 1
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, CountryName;
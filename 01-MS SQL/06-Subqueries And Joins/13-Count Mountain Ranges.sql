SELECT
	mc.CountryCode, COUNT(*) AS MountainRanges
FROM Mountains m
JOIN MountainsCountries mc ON m.Id = mc.MountainId
JOIN Countries c ON mc.CountryCode = c.CountryCode
WHERE mc.CountryCode IN ('US', 'BG', 'RU')
GROUP BY mc.CountryCode;
SELECT p.PeakName, r.RiverName, LOWER(CONCAT(p.PeakName, RIGHT(r.RiverName, LEN(r.RiverName) - 1))) AS [Mix]
FROM Peaks AS p,
	Rivers AS r
WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
ORDER BY [Mix];
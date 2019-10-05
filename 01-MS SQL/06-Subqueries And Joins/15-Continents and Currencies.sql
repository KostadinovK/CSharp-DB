SELECT 
	Result.ContinentCode,
	Result.CurrencyCode,
	Result.CurrencyUsage
FROM 
(
	SELECT 
		c.ContinentCode, c.CurrencyCode,
		COUNT(c.CurrencyCode) AS CurrencyUsage,
		DENSE_RANK() OVER(PARTITION BY c.ContinentCode ORDER BY COUNT(c.CurrencyCode) DESC) AS MostUsed 
	FROM Countries c
	GROUP BY c.ContinentCode, c.CurrencyCode
) AS Result
WHERE Result.CurrencyUsage > 1 AND Result.MostUsed = 1
ORDER BY Result.ContinentCode;
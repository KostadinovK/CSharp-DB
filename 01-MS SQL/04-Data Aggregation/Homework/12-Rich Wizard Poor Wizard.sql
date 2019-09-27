SELECT
	SUM(Result.Differences) AS SumDifference
FROM 
(
	SELECT
		DepositAmount - LEAD(DepositAmount) OVER (ORDER BY Id) AS Differences
	FROM WizzardDeposits
) AS Result;


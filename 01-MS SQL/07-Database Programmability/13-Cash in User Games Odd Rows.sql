CREATE FUNCTION ufn_CashInUsersGames(@name NVARCHAR(50))
RETURNS TABLE AS
RETURN 
(
	SELECT 
		SUM(Result.Cash) AS SumCash
	FROM
	(
		SELECT
			ug.GameId, ug.UserId, g.[Name], ug.Cash,
			ROW_NUMBER() OVER(PARTITION BY ug.GameId ORDER BY ug.Cash DESC) AS RowNumber
		FROM UsersGames ug
		JOIN Games g On ug.GameId = g.Id
		WHERE g.[Name] = @name
	) AS Result
	WHERE Result.RowNumber % 2 != 0 
)
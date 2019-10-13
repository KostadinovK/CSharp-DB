CREATE FUNCTION udf_UserTotalCommits(@username VARCHAR(30))
RETURNS INT AS
BEGIN
	DECLARE @commits INT = 
	(
		SELECT
			COUNT(*)
		FROM Users u
		JOIN Commits c ON u.Id = c.ContributorId
		WHERE u.Username = @username
	);
	RETURN @commits
END;
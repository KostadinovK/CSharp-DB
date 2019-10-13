SELECT
	u.Username, AVG(f.Size) AS Size
FROM Users u
JOIN Commits c ON u.Id = c.ContributorId
JOIN Files f ON c.Id = f.CommitId
GROUP BY u.Username
ORDER BY AVG(f.Size) DESC, u.Username;
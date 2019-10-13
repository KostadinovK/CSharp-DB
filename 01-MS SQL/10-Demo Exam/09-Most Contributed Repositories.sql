SELECT TOP(5)
	r.Id, r.[Name], COUNT(rc.ContributorId) AS [Commits]
FROM Repositories r
JOIN Commits c ON c.RepositoryId = r.Id
JOIN RepositoriesContributors rc ON r.Id = rc.RepositoryId
GROUP BY r.Id, r.[Name]
ORDER BY COUNT(rc.ContributorId) DESC, r.Id, r.[Name]
SELECT
	parents.Id, parents.[Name], CAST(parents.Size AS VARCHAR(10)) + 'KB' AS Size
FROM Files parents
LEFT JOIN Files child ON parents.Id = child.ParentId
WHERE child.ParentId IS NULL
ORDER BY parents.Id, parents.[Name], parents.Size
CREATE PROC usp_FindByExtension(@extension VARCHAR(10)) AS
BEGIN
	SELECT
		f.Id, f.[Name], CAST(f.Size AS VARCHAR(20)) + 'KB' AS Size
	FROM Files f
	WHERE CHARINDEX(@extension, f.[Name]) > 0
	ORDER BY f.Id, f.[Name], f.Size
END;
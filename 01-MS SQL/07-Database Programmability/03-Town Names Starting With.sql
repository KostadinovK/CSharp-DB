CREATE PROC usp_GetTownsStartingWith(@str VARCHAR(50)) AS
BEGIN
	SELECT 
		t.[Name] AS Town
	FROM Towns t
	WHERE LEFT(t.[Name], LEN(@str)) = @str;
END;
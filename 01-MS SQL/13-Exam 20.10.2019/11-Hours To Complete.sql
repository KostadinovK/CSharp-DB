CREATE FUNCTION udf_HoursToComplete(@StartDate DATETIME2, @EndDate DATETIME2)
RETURNS INT AS
BEGIN
	IF(@StartDate IS NULL OR @EndDate IS NULL)
	BEGIN
		RETURN 0
	END
	
	DECLARE @hours INT = 
	(
		SELECT DATEDIFF(HOUR, @StartDate, @EndDate)
	)

	RETURN @hours;
END;

SELECT dbo.udf_HoursToComplete(OpenDate, CloseDate) AS TotalHours
   FROM Reports

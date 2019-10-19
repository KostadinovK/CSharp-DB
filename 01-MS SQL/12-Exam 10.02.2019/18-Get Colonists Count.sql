CREATE FUNCTION udf_GetColonistsCount(@planetName VARCHAR(30))
RETURNS INT AS
BEGIN
	DECLARE @colonists INT = 
	(
		SELECT
			COUNT(*)
		FROM Planets p
		JOIN Spaceports s ON s.PlanetId = p.Id
		JOIN Journeys j ON j.DestinationSpaceportId = s.Id
		JOIN TravelCards t ON t.JourneyId = j.Id
		JOIN Colonists AS c ON c.Id = t.ColonistId
		WHERE p.[Name] = @planetName 
		GROUP BY p.[Name]
	)

	RETURN @colonists
END;

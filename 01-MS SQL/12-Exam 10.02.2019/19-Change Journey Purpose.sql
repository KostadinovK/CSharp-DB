CREATE PROC usp_ChangeJourneyPurpose(@journeyId INT, @newPurpose VARCHAR(30)) AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM Journeys WHERE Id = @journeyId))
	BEGIN
		;THROW 51000, 'The journey does not exist!', 1
	END

	IF(EXISTS(SELECT * FROM Journeys WHERE Id = @journeyId AND Purpose = @newPurpose))
	BEGIN
		;THROW 51000, 'You cannot change the purpose!', 2
	END
	
	UPDATE Journeys
		SET Purpose = @newPurpose
	WHERE Id = @journeyId
END;
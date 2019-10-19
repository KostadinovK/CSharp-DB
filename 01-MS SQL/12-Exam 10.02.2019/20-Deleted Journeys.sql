CREATE TABLE DeletedJourneys(
	Id INT PRIMARY KEY NOT NULL,
	JourneyStart DATETIME2 NOT NULL,
	JourneyEnd DATETIME2 NOT NULL,
	Purpose VARCHAR(11) NOT NULL,
	DestinationSpaceportId INT NOT NULL,
	SpaceshipId INT NOT NULL
);

CREATE TRIGGER tr_JouneysOnDelete ON Journeys AFTER DELETE
AS
	INSERT INTO DeletedJourneys SELECT * FROM deleted
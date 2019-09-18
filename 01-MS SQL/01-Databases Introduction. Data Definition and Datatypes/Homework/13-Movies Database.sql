CREATE DATABASE Movies;

USE Movies;

CREATE TABLE Directors(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(100)	
);

CREATE TABLE Genres(
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(100)
);

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(100)
);

CREATE TABLE Movies(
	Id INT PRIMARY KEY IDENTITY,
	Title NVARCHAR(30) NOT NULL,
	DirectorId INT NOT NULL FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear INT,
	[Length]  NVARCHAR(50) NOT NULL,
	GenreId INT NOT NULL FOREIGN KEY REFERENCES Genres(Id),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES Categories(Id),
	Rating INT,
	Notes NVARCHAR(100)
);

INSERT INTO Directors VALUES
('Steven Spielberg', NULL),
('Quentin Tarantino', NULL),
('Martin Scorsese', NULL),
('Alfred Hitchcock', NULL),
('Stanley Kubrick', NULL);

INSERT INTO Genres VALUES
('Action', NULL),
('Comedy', NULL),
('Drama', NULL),
('Horror', NULL),
('Romance', NULL);

INSERT INTO Categories VALUES
('Western', NULL),
('Martial Arts', NULL),
('Prison Escape', NULL),
('Car Racing', NULL),
('Cheating', NULL);

INSERT INTO Movies VALUES
('Movie 1', 1, 2018, '01:30:00', 2, 1, NULL, NULL),
('Movie 2', 4, 2008, '01:30:00', 1, 3, NULL, NULL),
('Movie 3', 2, 2011, '01:30:00', 2, 4, NULL, NULL),
('Movie 4', 2, 2012, '01:30:00', 4, 2, NULL, NULL),
('Movie 5', 1, 1992, '01:30:00', 3, 2, NULL, NULL);
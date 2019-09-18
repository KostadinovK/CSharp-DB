CREATE TABLE People(
	Id INT IDENTITY PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX),
	Height DECIMAL(15, 2),
	[Weight] DECIMAL(15, 2),
	Gender BIT NOT NULL,
	Birthdate DATE NOT NULL,
	Biography NTEXT
);

INSERT INTO People([Name], Picture, Height, [Weight], Gender, Birthdate, Biography) VALUES
	('Georgi Georgiev', NULL, 170.3, 70.5, 1, '1987-04-23', NULL),
	('Ivan Pashov', NULL, 172, 90, 1, '1999-12-21', NULL),
	('Maria Ilieva', NULL, 180.40, 80, 0, '1970-01-2', NULL),
	('Gosho', NULL, 180.40, 80, 1, '1990-03-23', NULL),
	('Pesho', NULL, 180.40, 80, 1, '1990-03-23', NULL);
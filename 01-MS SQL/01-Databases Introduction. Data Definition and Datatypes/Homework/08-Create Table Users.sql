CREATE TABLE Users(
	Id INT IDENTITY UNIQUE,
	Username VARCHAR(30) NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX),
	LastLoginTime DATETIME2,
	IsDeleted BIT
);

ALTER TABLE Users
ADD CONSTRAINT PK_UsersId PRIMARY KEY(Id)

ALTER TABLE Users 
ADD CONSTRAINT CH_ProfilePictureSize CHECK(DATALENGTH(ProfilePicture) <= 1024 * 900)

INSERT INTO Users VALUES
('test', 'test123', NULL, NULL, 0),
('admin', 'admin123', NULL, NULL, 0),
('ilko', 'ilko3', NULL, NULL, 1),
('pesho', 'test123', NULL, NULL, 0),
('gosho', 'test123', NULL, NULL, 0);
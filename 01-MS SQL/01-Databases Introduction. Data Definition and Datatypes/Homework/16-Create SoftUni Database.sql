CREATE DATABASE SoftUni;

USE SoftUni;

CREATE TABLE Towns(
	Id INT IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
);

ALTER TABLE Towns
ADD CONSTRAINT PK_Towns PRIMARY KEY(Id)

CREATE TABLE Addresses(
	Id INT IDENTITY,
	AddressText NVARCHAR(30) NOT NULL,
	TownId INT NOT NULL
);

ALTER TABLE Addresses
ADD CONSTRAINT PK_Addresses PRIMARY KEY(Id)

ALTER TABLE Addresses
ADD CONSTRAINT FK_AddressesToTowns FOREIGN KEY (TownId) REFERENCES Towns(Id)

CREATE TABLE Departments(
	Id INT IDENTITY,
	[Name] NVARCHAR(30) NOT NULL
);

ALTER TABLE Departments
ADD CONSTRAINT PK_Departments PRIMARY KEY(Id);

CREATE TABLE Employees(
	Id INT IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	MiddleName NVARCHAR(30),
	LastName NVARCHAR(30) NOT NULL,
	JobTitle NVARCHAR(30) NOT NULL,
	DepartmentId INT NOT NULL,
	HireDate DATE NOT NULL,
	Salary DECIMAL(15, 2) NOT NULL,
	AddressId INT
);

ALTER TABLE Employees
ADD CONSTRAINT PK_Employees PRIMARY KEY(Id);

ALTER TABLE Employees
ADD CONSTRAINT FK_Department FOREIGN KEY (DepartmentId) REFERENCES Departments(Id);

ALTER TABLE Employees
ADD CONSTRAINT FK_Address FOREIGN KEY (AddressId) REFERENCES Addresses(Id);
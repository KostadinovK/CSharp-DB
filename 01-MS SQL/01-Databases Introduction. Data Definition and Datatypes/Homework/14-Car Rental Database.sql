CREATE DATABASE CarRental;

USE CarRental;

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(40) NOT NULL,
	DailyRate INT,
	WeeklyRate INT,
	MonthlyRate INT,
	WeekendRate INT
);

INSERT INTO Categories VALUES
('Category 1', 8, 7, 7, 7),
('Category 2', 8, 7, 7, 7),
('Category 3', 2, 3, 3, NULL);

CREATE TABLE Cars(
	Id INT PRIMARY KEY IDENTITY,
	PlateNumber NVARCHAR(10) NOT NULL,
	Manufacturer NVARCHAR(30) NOT NULL,
	Model NVARCHAR(30) NOT NULL,
	CarYear INT NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id),
	Doors INT,
	Picture VARBINARY(MAX),
	Condition NVARCHAR(40),
	Available BIT NOT NULL
);

INSERT INTO Cars VALUES
('CA3431OB', 'Opel', 'Astra', 2009, 1, 4, NULL, NULL, 1),
('CB1091BA', 'BMW', 'X5', 2019, 1, 4, NULL, NULL, 1),
('CA3991OB', 'Mazda', '6', 2012, 1, 4, NULL, NULL, 0);

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Title NVARCHAR(30) NOT NULL,
	Notes NVARCHAR(100)
);

INSERT INTO Employees VALUES
('Test', 'Testov', 'Software Developer', NULL),
('Pesho', 'Peshev', 'Manager', NULL),
('Ivan', 'Ivanov', 'Trader', NULL);

CREATE TABLE Customers(
	Id INT PRIMARY KEY IDENTITY,
	DriverLicenseNumber NVARCHAR(20) NOT NULL,
	FullName NVARCHAR(40) NOT NULL,
	[Address] NVARCHAR(40) NOT NULL,
	City NVARCHAR(20) NOT NULL,
	ZIPCode INT,
	Notes NVARCHAR(100)
);

INSERT INTO Customers VALUES
('034A239AD', 'Test Testov', 'Test Street Num.8', 'Test City', 1099, NULL),
('034A277SD', 'Admin Adminov', 'Admin Street Num.2', 'Admin City', 1019, NULL),
('233D239AD', 'Gosho Ivanov', 'Test Street Num.8', 'Test City', 1099, NULL);

CREATE TABLE RentalOrders(
	Id INT PRIMARY KEY IDENTITY,
	EmployeeId INT FOREIGN KEY REFERENCES Employees(Id),
	CustomerId INT FOREIGN KEY REFERENCES Customers(Id),
	CarId INT FOREIGN KEY REFERENCES Cars(Id),
	TankLevel INT NOT NULL,
	KilometrageStart INT NOT NULL,
	KilometrageEnd INT NOT NULL,
	TotalKilometrage INT NOT NULL,
	StartDate DATE NOT NULL,
	EndDate DATE NOT NULL,
	TotalDays INT,
	RateApplied DECIMAL(18, 2) NOT NULL,
	TaxRate DECIMAL(18, 2) NOT NULL,
	OrderStatus NVARCHAR(20) NOT NULL,
	Notes NVARCHAR(100)
);

INSERT INTO RentalOrders VALUES
(1, 1, 1, 100, 10000, 10200, 200, '2019-01-01', '2019-01-05', 5, 200, 40, 'Completed', NULL),
(2, 2, 2, 70, 1000, 1100, 100, '2019-01-01', '2019-01-03', 3, 120, 40, 'Completed', NULL),
(3, 3, 3, 100, 10, 50, 40, '2019-03-04', '2019-03-05', 1, 20, 20, 'Completed', NULL);

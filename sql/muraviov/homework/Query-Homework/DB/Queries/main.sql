USE Academy
GO


/*

CREATE TABLE [Groups] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24),
	[Year] DATE,
	[DepartmentId] INT

)

*/


--/*


CREATE TABLE [Teachers] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY NOT NULL,
	[EmploymentDate] DATE NOT NULL,
	FirstName NVARCHAR(24) NOT NULL,
	SecondName NVARCHAR(24) NOT NULL,
	Salary MONEY NOT NULL,
	Premium MONEY NOT NULL,

)

CREATE TABLE [Groups] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(24) NOT NULL,
	[Year] DATE NOT NULL,
	[Raiting] INT NOT NULL

)

CREATE TABLE [Departments] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY NOT NULL,
	[Financing] NVARCHAR(24) NOT NULL,
	[Name] NVARCHAR(24) NOT NULL

)

CREATE TABLE [Falculties] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(24) NOT NULL

)


--*/


/*

CREATE TABLE [GroupsLectures] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[GroupId] INT,
	[LectureId] INT

)

*/


/*

CREATE TABLE [Lectures] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Date] DATE,
	[SubjectId] INT,
	[TeacherId] INT

)

*/


/*

CREATE TABLE [Subjects] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24)

)

*/


/*

CREATE TABLE [Students] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24),
	[Rating] INT,
	[Surname] NVARCHAR(24)

)

*/


/*

CREATE TABLE [GroupsStudents] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[GroupId] INT,
	[StudentId] INT

)

*/


/*

CREATE TABLE [GroupsCourators] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[GroupId] INT,
	[CuratorId] INT

)

*/


/*

CREATE TABLE [Falculties] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24)

)

*/


/*

CREATE TABLE [Departments] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Building] INT,
	[Financing] NVARCHAR(24),
	[Name] NVARCHAR(24),
	[FacultyId] INT

)

*/


/*

CREATE TABLE [Courators] (

	[Id] INT IDENTITY(0,1) PRIMARY KEY,
	[Name] NVARCHAR(24),
	[Surname] NVARCHAR(24)
)

*/






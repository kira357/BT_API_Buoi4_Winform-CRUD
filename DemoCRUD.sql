Create database DemoCRUD
go
use DemoCRUD
go
create table StudentsTb(
	StudentID int primary key IDENTITY(1,1) not null,
	Name nvarchar(20),
	FatherName nvarchar(20),
	RollNumber nvarchar(20),
	Address nvarchar(20),
	Mobile nvarchar(20) 
)
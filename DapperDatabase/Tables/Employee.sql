CREATE TABLE [dbo].[Employee]
(
	[EmployeeID] INT Identity(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](50) NOT NULL,
	[DepartmentId] int not null ,
	constraint [FK_Employee_Department] foreign key([DepartmentId])
	references [dbo].[Department]([DepartmentID])
)

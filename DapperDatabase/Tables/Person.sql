CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL IDentity(1,1) PRIMARY KEY,
    [AddressId] int  null,
	[Title] NVARCHAR(50) NULL, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [DateOfBirth] DATETIME NULL,
    constraint [FK_Person_Address] foreign key(AddressId)
    references [dbo].[Address](Id)

)

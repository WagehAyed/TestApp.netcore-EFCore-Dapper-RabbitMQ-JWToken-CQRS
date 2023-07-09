CREATE TABLE [dbo].[Category]
(
	[CategoryId] INT identity(1,1) NOT NULL PRIMARY KEY, 
    [CategoryName] NVARCHAR(150) NULL, 
    [Description] NVARCHAR(150) NULL
)

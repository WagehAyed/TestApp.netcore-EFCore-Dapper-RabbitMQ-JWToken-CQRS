CREATE TABLE [dbo].[Category]
(
	[CategoryId] INT identity(1,1) NOT NULL PRIMARY KEY, 
    [CategoryName] NVARCHAR(50) NULL, 
    [Description] NVARCHAR(50) NULL
)

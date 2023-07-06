CREATE TABLE [dbo].[Product]
(
	[ProductId] INT identity(1,1) NOT NULL PRIMARY KEY, 
    [CategoryId] INT NULL, 
    [ProductName] NVARCHAR(50) NULL, 
    [UnitPrice] DECIMAL(18, 2) NULL, 
    [UnitsInStock] INT NULL, 
    [UnitsInOrder] INT NULL,
    constraint [FK_Product_Category] foreign key(CategoryId)
    references [dbo].[Category](CategoryId)
)

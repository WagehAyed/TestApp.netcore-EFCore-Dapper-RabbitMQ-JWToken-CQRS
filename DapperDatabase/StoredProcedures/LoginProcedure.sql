CREATE PROCEDURE LoginProcedure
@flag nvarchar(50),
@id int=null
AS
BEGIN
	
	if @flag ='read'
	Begin
	select * from Login;
	end
	else if @flag ='delete'
	Begin
	Delete From Login Where Id=@id;
	end
END
GO

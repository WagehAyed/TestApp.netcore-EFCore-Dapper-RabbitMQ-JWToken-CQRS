﻿/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
:r .\Login.seed.sql
:r .\Department.Seed.sql
:r .\Employee.Seed.sql 
--:r .\Category.Seed.sql
--:r .\Product.Seed.sql
:r .\AdressAndPerson.Seed.sql
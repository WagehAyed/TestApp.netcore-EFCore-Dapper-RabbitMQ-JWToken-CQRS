Merge Login As Target
using (
select * from (
SELECT TOP 20 'user' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as nvarchar) + '@example.com' as Email, 'password' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as nvarchar)  as Password
FROM sys.all_columns
) as t
) as source
on (source.Email=target.Email)
when not matched by target
Then insert (Email,Password) values(source.Email,source.password);
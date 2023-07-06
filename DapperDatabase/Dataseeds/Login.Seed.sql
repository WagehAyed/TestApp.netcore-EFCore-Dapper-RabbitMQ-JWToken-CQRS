Merge Login As Target
using (
select * from (
SELECT   'user' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as nvarchar) + '@example.com' as Email, 'password' + CAST(ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) as nvarchar)  as Password
FROM sys.all_columns 
order by object_id
OFFSET 20 ROWS
FETCH NEXT 80 ROWS ONLY
) as t
) as source
on (source.Email=target.Email)
when not matched by target
Then insert (Email,Password) values(source.Email,source.password);
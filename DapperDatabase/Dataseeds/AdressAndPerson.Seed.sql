-- insert data into the Address table
INSERT INTO [dbo].[Address] ([Country], [PostCode], [Street], [Ciry])
VALUES ('USA', '12345', 'Main St', 'New York'),
       ('Canada', '67890', 'Maple Ave', 'Toronto'),
       ('UK', '11111', 'High St', 'London');

-- insert data into the Person table
INSERT INTO [dbo].[Person] ([AddressId], [Title], [FirstName], [LastName], [DateOfBirth])
VALUES (1, 'Mr', 'John', 'Doe', '1980-01-01'),
       (2, 'Ms', 'Jane', 'Smith', '1990-02-02'),
       (3, 'Dr', 'David', 'Jones', '1975-03-03');
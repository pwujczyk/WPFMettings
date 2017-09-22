CREATE TABLE [Contact] (
  [ContactId] int IDENTITY (1,1) NOT NULL,
  FirstName NVARCHAR(20),
  LastName NVARCHAR(20),
  Email NVARCHAR(50)
);
GO
ALTER TABLE [Contact] ADD CONSTRAINT [PK_Contact] PRIMARY KEY ([ContactId]);
GO

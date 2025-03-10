IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = N'fedemo')
BEGIN
  CREATE DATABASE fedemo;
END;
GO

USE fedemo;
GO

-- Create the Contacts table
IF OBJECT_ID(N'PostalCodes', N'U') IS NULL
BEGIN
    CREATE TABLE PostalCodes
    (
        Id            INT PRIMARY KEY IDENTITY(1,1) ,
        Code          VARCHAR(6) NOT NULL,
        Street        VARCHAR(100) NOT NULL,
        City          VARCHAR(100) NOT NULL,
        HouseNumber   VARCHAR(12) NOT NULL
    );
END;
GO

IF (SELECT COUNT(*) FROM PostalCodes) = 0
BEGIN

  DECLARE @postalcodes VARCHAR(MAX)
  SELECT @postalcodes = BulkColumn
  FROM OPENROWSET(BULK '/mnt/sql-init/apeldoorn.json', SINGLE_CLOB) AS json;

  INSERT INTO PostalCodes (Code, Street, City, HouseNumber)
  SELECT Code, Street, City, HouseNumber FROM OPENJSON(@postalcodes, '$.features')
  WITH (
      Code VARCHAR(6) '$.properties.postcode',
      Street VARCHAR(100) '$.properties.openbareruimtenaam',
      City VARCHAR(100) '$.properties.woonplaatsnaam',
      HouseNumber VARCHAR(12) '$.properties.huisnummer'
  )
END

  
IF OBJECT_ID(N'Recipes', N'U') IS NULL
BEGIN
    CREATE TABLE Recipes
    (
        Id            INT PRIMARY KEY IDENTITY(1,1),
        Name          VARCHAR(100) NOT NULL,
        Description   VARCHAR(MAX) NULL,
        Ingredients   VARCHAR(MAX) NULL,
        Instructions  VARCHAR(MAX) NULL,
        IsSecret      BIT NULL
    );
END;
GO
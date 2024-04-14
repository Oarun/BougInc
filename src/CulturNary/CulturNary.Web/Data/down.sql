-- Step 3: Drop the Recipes table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Recipes_Collections')
BEGIN
    ALTER TABLE Recipes
    DROP CONSTRAINT FK_Recipes_Collections; -- Drop the foreign key constraint referencing Collections
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Recipes_Person')
BEGIN
    ALTER TABLE Recipes
    DROP CONSTRAINT FK_Recipes_Person; -- Drop the foreign key constraint referencing Person
END

DROP TABLE IF EXISTS Recipes;

-- Step 2: Drop the Collections table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Collections_Person')
BEGIN
    ALTER TABLE Collections
    DROP CONSTRAINT FK_Collections_Person; -- Drop the foreign key constraint referencing Person
END

DROP TABLE IF EXISTS Collections;

-- Step 1: Drop the Person table
DROP TABLE IF EXISTS Person;

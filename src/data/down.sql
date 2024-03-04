-- Drop the foreign key constraint in Recipes table
ALTER TABLE Recipes DROP CONSTRAINT FK_Recipes_Person;

-- Drop the foreign key constraint in Collections table
ALTER TABLE Collections DROP CONSTRAINT FK_Collections_Person;

-- Drop the Recipes table
DROP TABLE Recipes;

-- Drop the Collections table
DROP TABLE Collections;

-- Drop the Person table
DROP TABLE Person;
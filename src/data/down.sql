-- Drop foreign key constraints in PersonFood table if it exists
IF OBJECT_ID('PersonFood', 'U') IS NOT NULL
BEGIN
    ALTER TABLE PersonFood DROP CONSTRAINT IF EXISTS FK_PersonFood_Person;
    DROP TABLE PersonFood;
END;

-- Drop foreign key constraints in PersonRecipe table if it exists
IF OBJECT_ID('PersonRecipe', 'U') IS NOT NULL
BEGIN
    ALTER TABLE PersonRecipe DROP CONSTRAINT IF EXISTS FK_PersonRecipe_Person;
    DROP TABLE PersonRecipe;
END;

-- Drop foreign key constraints in RecipeIngredient table if it exists
IF OBJECT_ID('RecipeIngredient', 'U') IS NOT NULL
BEGIN
    ALTER TABLE RecipeIngredient DROP CONSTRAINT IF EXISTS FK_RecipeIngredient_Recipes;
    ALTER TABLE RecipeIngredient DROP CONSTRAINT IF EXISTS FK_RecipeIngredient_Foods;
    DROP TABLE RecipeIngredient;
END;

-- Drop the Recipes table if it exists
IF OBJECT_ID('Recipes', 'U') IS NOT NULL
BEGIN
    DROP TABLE Recipes;
END;

-- Drop the Foods table if it exists
IF OBJECT_ID('Foods', 'U') IS NOT NULL
BEGIN
    DROP TABLE Foods;
END;

-- Drop the Person table if it exists
IF OBJECT_ID('Person', 'U') IS NOT NULL
BEGIN
    DROP TABLE Person;
END;

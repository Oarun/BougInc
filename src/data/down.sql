-- Step 9: Drop the Friendship table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Friendship_Person1Id')
BEGIN
    ALTER TABLE Friendship
    DROP CONSTRAINT FK_Friendship_Person1Id; -- Drop the foreign key constraint referencing Person (Person1Id)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Friendship_Person2Id')
BEGIN
    ALTER TABLE Friendship
    DROP CONSTRAINT FK_Friendship_Person2Id; -- Drop the foreign key constraint referencing Person (Person2Id)
END

DROP TABLE IF EXISTS Friendship;

-- Step 8: Drop the FriendRequest table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_FriendRequest_RecipientId')
BEGIN
    ALTER TABLE FriendRequest
    DROP CONSTRAINT FK_FriendRequest_RecipientId; -- Drop the foreign key constraint referencing Person (RecipientId)
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_FriendRequest_RequesterId')
BEGIN
    ALTER TABLE FriendRequest
    DROP CONSTRAINT FK_FriendRequest_RequesterId; -- Drop the foreign key constraint referencing Person (RequesterId)
END

DROP TABLE IF EXISTS FriendRequest;

-- Step 7: Drop the Restaurants table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Restaurants_Person')
BEGIN
    ALTER TABLE Restaurants
    DROP CONSTRAINT FK_Restaurants_Person; -- Drop the foreign key constraint referencing Person
END

DROP TABLE IF EXISTS Restaurants;

-- Step 6: Drop the Videos table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Videos_Person')
BEGIN
    ALTER TABLE Videos
    DROP CONSTRAINT FK_Videos_Person; -- Drop the foreign key constraint referencing Person
END

DROP TABLE IF EXISTS Videos;

-- Step 5: Drop the FavoriteRecipe table
DROP TABLE IF EXISTS FavoriteRecipe;

-- Step 4: Drop the Recipes table
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

-- Step 3: Drop the Collections table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE CONSTRAINT_NAME = 'FK_Collections_Person')
BEGIN
    ALTER TABLE Collections
    DROP CONSTRAINT FK_Collections_Person; -- Drop the foreign key constraint referencing Person
END

DROP TABLE IF EXISTS Collections;

-- Step 2: Drop the Person table
DROP TABLE IF EXISTS Person;

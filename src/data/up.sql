CREATE TABLE Person (
    id INT PRIMARY KEY IDENTITY(1,1),
    identity_id NVARCHAR(450) UNIQUE,
    -- first_name VARCHAR(50) NOT NULL,
    -- last_name VARCHAR(50) NOT NULL,
    -- username VARCHAR(50) NOT NULL UNIQUE,
    -- email VARCHAR(255) NOT NULL UNIQUE,
    -- dietary_restrictions VARCHAR(255),
    -- food_preferences VARCHAR(255),
    -- health_goals VARCHAR(255)
);

-- CREATE TABLE Foods (
--     id INT PRIMARY KEY IDENTITY(1,1),
--     name VARCHAR(255) NOT NULL,
--     description VARCHAR(500),
--     serving_size VARCHAR(50),
--     calories DECIMAL(6,2) NOT NULL,
--     fat DECIMAL(6,2) NOT NULL,
--     protein DECIMAL(6,2) NOT NULL,
--     carbs DECIMAL(6,2),
--     sugar DECIMAL(6,2),
--     ingredients VARCHAR(500)
-- );

CREATE TABLE Collections (
    id INT PRIMARY Key IDENTITY (1,1),
    person_id INT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(500),
    FOREIGN KEY (person_id) REFERENCES Person(id),
);

CREATE TABLE Recipes (
    id INT PRIMARY KEY IDENTITY(1,1),
    collection_id int,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(500),
    -- instructions TEXT NOT NULL,
    -- cooking_time INT,
    -- serving_size VARCHAR(50),
    -- calories DECIMAL(6,2),
    -- fat DECIMAL(6,2),
    -- protein DECIMAL(6,2),
    -- carbs DECIMAL(6,2),
    -- sugar DECIMAL(6,2),
    -- category VARCHAR(50),
    -- cuisine VARCHAR(50),
    -- dietary_restrictions VARCHAR(255)
    FOREIGN KEY (collection_id) REFERENCES Collections(id),
);

-- CREATE TABLE PersonFood (
--     id INT PRIMARY KEY IDENTITY(1,1),
--     person_id INT,
--     food_id INT,
--     FOREIGN KEY (person_id) REFERENCES Person(id),
--     FOREIGN KEY (food_id) REFERENCES Foods(id)
-- );

-- CREATE TABLE RecipeIngredient (
--     id INT PRIMARY KEY IDENTITY(1,1),
--     recipe_id INT,
--     food_id INT,
--     quantity VARCHAR(50),
--     FOREIGN KEY (recipe_id) REFERENCES Recipes(id),
--     FOREIGN KEY (food_id) REFERENCES Foods(id)
-- );

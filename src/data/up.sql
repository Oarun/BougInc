-- Person table
CREATE TABLE Person (
  id INT PRIMARY KEY IDENTITY(1,1),
  username VARCHAR(50) NOT NULL UNIQUE,
  email VARCHAR(255) NOT NULL UNIQUE,
  password VARCHAR(255) NOT NULL,
  dietary_restrictions VARCHAR(255),
  food_preferences VARCHAR(255),
  health_goals VARCHAR(255)
);

-- Foods table
CREATE TABLE Foods (
  id INT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(255) NOT NULL,
  description VARCHAR(500),
  serving_size VARCHAR(50),
  calories DECIMAL(6,2) NOT NULL,
  fat DECIMAL(6,2) NOT NULL,
  protein DECIMAL(6,2) NOT NULL,
  carbs DECIMAL(6,2),
  sugar DECIMAL(6,2),
  ingredients VARCHAR(500)
);

-- Recipes table
CREATE TABLE Recipes (
  id INT PRIMARY KEY IDENTITY(1,1),
  name VARCHAR(255) NOT NULL,
  description VARCHAR(500),
  instructions TEXT NOT NULL,
  cooking_time INT,
  serving_size VARCHAR(50),
  calories DECIMAL(6,2),
  fat DECIMAL(6,2),
  protein DECIMAL(6,2),
  carbs DECIMAL(6,2),
  sugar DECIMAL(6,2),
  images VARCHAR(255),
  category VARCHAR(50),
  cuisine VARCHAR(50),
  dietary_restrictions VARCHAR(255)
);

-- PersonFood table (many-to-many relationship between people and foods)
CREATE TABLE PersonFood (
  person_id INT FOREIGN KEY REFERENCES Person(id),
  food_id INT FOREIGN KEY REFERENCES Foods(id),
  PRIMARY KEY (person_id, food_id)
);

-- PersonRecipe table (many-to-many relationship between people and recipes)
CREATE TABLE PersonRecipe (
  person_id INT FOREIGN KEY REFERENCES Person(id),
  recipe_id INT FOREIGN KEY REFERENCES Recipes(id),
  PRIMARY KEY (person_id, recipe_id)
);

-- RecipeIngredient table (many-to-many relationship between recipes and ingredients)
CREATE TABLE RecipeIngredient (
  recipe_id INT FOREIGN KEY REFERENCES Recipes(id),
  food_id INT FOREIGN KEY REFERENCES Foods(id),
  quantity VARCHAR(50),
  PRIMARY KEY (recipe_id, food_id)
);
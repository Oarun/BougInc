-- Create the Person table
CREATE TABLE Person (
    id INT PRIMARY KEY IDENTITY(1,1),
    identity_id NVARCHAR(450) UNIQUE
    -- Add other columns as needed
);

-- Create the Collections table
CREATE TABLE Collections (
    id INT PRIMARY Key IDENTITY (1,1),
    person_id INT,
    name VARCHAR(255) NOT NULL,
    tags VARCHAR(1000),
    description VARCHAR(MAX),
    img VARCHAR(MAX),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);

-- Create the Recipes table
CREATE TABLE Recipes (
    id INT PRIMARY KEY IDENTITY(1,1),
    collection_id INT,
    person_id INT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(MAX),
    img VARCHAR(MAX),
    uri VARCHAR(MAX),
    FOREIGN KEY (collection_id) REFERENCES Collections(id),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);

-- Create the FavoriteRecipe table
CREATE TABLE FavoriteRecipe (
    id INT PRIMARY KEY IDENTITY(1,1),
    person_id INT NOT NULL,
    recipe_id INT,
    favorite_date DATETIME NOT NULL,
    ImageUrl VARCHAR(MAX),
    Label VARCHAR(MAX),
    Uri VARCHAR(MAX),
    tags VARCHAR(MAX),
    FOREIGN KEY (person_id) REFERENCES Person(id),
    FOREIGN KEY (recipe_id) REFERENCES Recipes(id)
);

-- Create the Videos table
CREATE TABLE Videos (
    id INT PRIMARY KEY IDENTITY(1,1),
    person_id INT,
    video_name VARCHAR(255) NOT NULL,
    video_type VARCHAR(100),
    video_link VARCHAR(MAX),
    video_notes VARCHAR(MAX),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);

-- Create the Restaurants table
CREATE TABLE Restaurants (
    id INT PRIMARY KEY IDENTITY(1,1),
    person_id INT,
    restaurants_name VARCHAR(255) NOT NULL,
    restaurants_address VARCHAR(MAX),
    restaurants_website VARCHAR(MAX),
    restaurants_menu VARCHAR(MAX),
    restaurants_phone_number VARCHAR(20),
    restaurants_notes VARCHAR(MAX),
    restaurant_type VARCHAR(100),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);

-- Create the FriendRequest table
CREATE TABLE FriendRequest (
    Id INT PRIMARY KEY IDENTITY(1,1),
    RequesterId INT,
    RecipientId INT,
    Status VARCHAR(255),
    RequestDate DATETIME,
    ResponseDate DATETIME,
    FOREIGN KEY (RecipientId) REFERENCES Person(id),
    FOREIGN KEY (RequesterId) REFERENCES Person(id)
);

-- Create the Friendship table
CREATE TABLE Friendship (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Person1Id INT,
    Person2Id INT,
    FriendshipDate DATETIME,
    FOREIGN KEY (Person1Id) REFERENCES Person(id),
    FOREIGN KEY (Person2Id) REFERENCES Person(id)
);

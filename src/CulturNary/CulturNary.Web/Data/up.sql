-- Step 1: Create the Person table
CREATE TABLE Person (
    id INT PRIMARY KEY IDENTITY(1,1),
    identity_id NVARCHAR(450) UNIQUE
    -- Add other columns as needed
);

-- Step 2: Create the Collections table
CREATE TABLE Collections (
    id INT PRIMARY Key IDENTITY (1,1),
    person_id INT,
    name VARCHAR(255) NOT NULL,
    tags VARCHAR(1000),
    description VARCHAR(MAX),
    img VARCHAR(MAX),
    FOREIGN KEY (person_id) REFERENCES Person(id)
);

-- Step 3: Create the Recipes table
CREATE TABLE Recipes (
    id INT PRIMARY KEY IDENTITY(1,1),
    collection_id INT,
    person_id INT, -- New column
    name VARCHAR(255) NOT NULL,
    description VARCHAR(MAX),
    img VARCHAR(MAX),
    uri VARCHAR(MAX),
    FOREIGN KEY (collection_id) REFERENCES Collections(id),
    FOREIGN KEY (person_id) REFERENCES Person(id) -- New foreign key constraint
);

-- Step 4: Create the Accept table
CREATE TABLE [Accept] (
    AcceptId INT PRIMARY KEY
);

-- Step 5: Create the All table
CREATE TABLE [All] (
    AllId INT PRIMARY KEY,
    AcceptId INT,
    FOREIGN KEY (AcceptId) REFERENCES [Accept](AcceptId)
);

-- Step 6: Create the MealTimeSection table
CREATE TABLE MealTimeSection (
    MealTimeSectionId INT PRIMARY KEY,
    AcceptId INT,
    FOREIGN KEY (AcceptId) REFERENCES [Accept](AcceptId)
);

-- Step 7: Create the ENERCKCAL table
CREATE TABLE ENERCKCAL (
    ENERCKCALId INT PRIMARY KEY,
    min INT,
    max INT
);

-- Step 8: Create the Fit table
CREATE TABLE Fit (
    FitId INT PRIMARY KEY,
    ENERCKCALId INT,
    SUGARAddedMax INT,
    FOREIGN KEY (ENERCKCALId) REFERENCES ENERCKCAL(ENERCKCALId)
);

-- Step 9: Create the Sections table
CREATE TABLE Sections (
    SectionsId INT PRIMARY KEY,
    BreakfastId INT,
    LunchId INT,
    DinnerId INT,
    FOREIGN KEY (BreakfastId) REFERENCES MealTimeSection(MealTimeSectionId),
    FOREIGN KEY (LunchId) REFERENCES MealTimeSection(MealTimeSectionId),
    FOREIGN KEY (DinnerId) REFERENCES MealTimeSection(MealTimeSectionId)
);

-- Step 10: Create the Plan table
CREATE TABLE [Plan] (
    PlanId INT PRIMARY KEY,
    AcceptId INT,
    FitId INT,
    SectionsId INT,
    FOREIGN KEY (AcceptId) REFERENCES [Accept](AcceptId),
    FOREIGN KEY (FitId) REFERENCES Fit(FitId),
    FOREIGN KEY (SectionsId) REFERENCES Sections(SectionsId)
);

-- Step 11: Create the MealPlan table
CREATE TABLE MealPlan (
    MealPlanId INT PRIMARY KEY,
    id INT,
    personId INT,
    size INT,
    PlanId INT,
    Details NVARCHAR(1000) NOT NULL,
    FOREIGN KEY (PlanId) REFERENCES [Plan](PlanId)
);

-- Step 12: Create the MealPlanRequest table
CREATE TABLE MealPlanRequest (
    MealPlanRequestId INT PRIMARY KEY,
    CalorieMin INT,
    CalorieMax INT
);

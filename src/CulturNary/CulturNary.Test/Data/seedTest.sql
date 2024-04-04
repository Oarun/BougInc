INSERT INTO Person(Id, identity_id) VALUES (1, "pOne"), (2, "pTwo"), (3, "pThree"), (8, "pFour"), (4, "pFive");

INSERT INTO Collections(Id, Person_Id, Name, Tags, Description, Img) VALUES 
(1, 1, 'My Recipes', 'cooking, recipes', 'A collection of my favorite recipes', 'recipe_img.jpg'),
(2, 1, 'Desserts', 'desserts, sweets', 'Collection of dessert recipes', 'dessert_img.jpg'),
(3, 3, 'Healthy Meals', 'healthy, meals', 'Healthy meal recipes', 'healthy_img.jpg');

INSERT INTO Recipes(Collection_Id, Person_Id, Name, Description, Img) VALUES 
(1, 1, 'Spaghetti Carbonara', 'Classic Italian pasta dish made with eggs, cheese, pancetta, and black pepper.', 'carbonara_img.jpg'),
(1, 1, 'Chicken Parmesan', 'Breaded chicken topped with marinara sauce and melted cheese, served over spaghetti.', 'chicken_parm_img.jpg'),
(2, 1, 'Chocolate Cake', 'Decadent chocolate cake with rich frosting.', 'chocolate_cake_img.jpg'),
(2, 1, 'Apple Pie', 'Traditional American pie made with apples and cinnamon.', 'apple_pie_img.jpg'),
(3, 2, 'Quinoa Salad', 'Healthy salad made with quinoa, vegetables, and vinaigrette dressing.', 'quinoa_salad_img.jpg');

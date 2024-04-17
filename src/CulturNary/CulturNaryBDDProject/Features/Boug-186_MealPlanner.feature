Feature: Boug-186_MealPlanner

As a user I can access a page where I can generate meal plans by selecting health and dietary based preferences

@mealPlanner
Scenario: When I am on the home page I can see an active link to the meal planner
	Given I am on the home page
	When I look at the navigation bar
	Then I can see a link to the meal planner

@mealPlanner
Scenario: When I click on the meal planner link I am taken to the meal planner page
	Given I am on the home page
	When I click on the meal planner link
	Then I am taken to the meal planner page

@mealPlanner
Scenario: When I am on the meal planner page I see and select health and dietary preferences and settings
	Given I am on the meal planner page
	Then I can see health and dietary preferences and settings
	And I can select health and dietary preferences and settings

@mealPlanner
Scenario: When I am on the meal planner page I can enter minimum and maximum calories
	Given I am on the meal planner page
	Then I can enter minimum and maximum calories
@AustinDavis
Feature: User Collections (Boug-157)

Logged in users should be able to view their own collections page, which should contain their collections, recipes, and search similar recipes.

The users should also have the ability to add, edit, and delete collection and recipes. 


Background:
    Given the following user exists
        | UserName | Password | DisplayName | Biography | ProfileImageName |
        | testuser@fakemail.com | password123 | Test User    | Hello!    | scrungle.jpg |
    And I login as 'testuser@fakemail.com' with password 'password123'

Scenario: Colletion Page contains existing user information

    Given I am a logged in user
    When I go to the user collection page
    Then I should see a form to create a collection

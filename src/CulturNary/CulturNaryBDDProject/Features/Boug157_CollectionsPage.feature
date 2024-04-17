@AustinDavis
Feature: User Collections (Boug-157)

Logged in users should be able to view their own collections page, which should contain their collections, recipes, and search similar recipes.

The users should also have the ability to add, edit, and delete collection and recipes. 


Background:
        Given the following user exists in BougOneFiftySeven
            | UserName | Password | DisplayName | Biography | ProfileImageName |
            | testuser@fakemail.com | Password123! | Test User    | Hello!    | scrungle.jpg |
    

Scenario: As a signed in user, when I go to the Collections page I should see a form to add a collection.

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user collection page
    Then I should see a form to create a collection

Scenario: As a signed in user, when I go to the Collections page there should be a list of my collections.

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user collection page
    Then I should see my collections

Scenario: As a signed in user, when I go to the Collections page I should see logo to delete a collection

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user collection page
    Then I should see a delete logo to delete a collection

Scenario: As a signed in user, when I go to the Collections page I should see logo to edit a collection

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user collection page
    Then I should see a edit logo to edit a collection
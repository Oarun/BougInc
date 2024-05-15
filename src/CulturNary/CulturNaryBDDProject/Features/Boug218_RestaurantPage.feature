@AustinDavis
Feature: User Collections (Boug-218)

Logged in users should be able to view their own restaurants page, which should contain their saved restaurants and a form to add restaurants.
The user should also see a map that gives them the ability to see the locations of thier restaurants as well as the ability to find new ones.


Background:
        Given the following user exists in BougOneFiftySeven
            | UserName | Password | DisplayName | Biography | ProfileImageName |
            | testuser@fakemail.com | Password123! | Test User    | Hello!    | scrungle.jpg |

Scenario: As a signed in user, when I go to the Restaurants page I should see a form to add a restaurant.

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user restaurant page
    Then I should see a form to create a collection

Scenario: As a signed in user, when I go to the Restaurants page and have saved restaurants, I should see a list of restaurants.

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user restaurant page
    Then I should see my restaurants list

Scenario: As a signed in user, when I go to the Restaurants page I should see a button to view my saved restaurants on a map

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user restaurant page
    Then I should see a form to enter my zipcode

Scenario: As a signed in user, when I go to the Restaurants page I should see a form to enter a zipcode.

    Given the user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
    When I go to the user restaurant page
    Then I should see a button to show my restaurant list on map
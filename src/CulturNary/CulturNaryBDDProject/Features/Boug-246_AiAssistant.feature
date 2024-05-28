Feature: AI Assistant (Boug-246)

OpenAI integration for comparing recipe within a url to user's dietary restrictions.

    Background:
        Given the following user exists in BougTwoFourtySix
        | UserName | Password | DisplayName | Biography | ProfileImageName |
        | testuser@fakemail.com | Password123! | Test User    | Hello!    | scrungle.jpg |

    Scenario: User compares a recipe to their dietary restrictions
        Given a user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
        When the user navigates to the AI Assistant page
        And the user enters and submits a url to a recipe
        Then the user is shown a comparison of the recipe to their dietary restrictions
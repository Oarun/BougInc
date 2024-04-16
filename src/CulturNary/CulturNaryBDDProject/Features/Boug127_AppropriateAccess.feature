Feature: Appropriate Access (Boug-127)

Logged in users should be able to access logged in user information and can't access unsigned pages

And also the opposite, non-logged in users should be able to access non-logged in user information and can't access logged in pages
    Background:
        Given the following user exists
            | UserName | Password | DisplayName | Biography | ProfileImageName |
            | testuser@fakemail.com | Password123! | Test User    | Hello!    | scrongle.jpg |

    Scenario: Signed-in user attempts to access non-signed-in user pages
        Given a user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
        When the signed-in user tries to access non-signed-in user pages
        Then the system should prevent access and redirect the user to the appropriate page

    Scenario: Non-signed-in user attempts to access signed-in user pages
        Given a non-signed-in user is not logged into the system
        When the non-signed-in user tries to access signed-in user pages
        Then the system should prevent access and redirect the user to the login page

    Scenario: Signed-in user attempts to access registration or login page
        Given a user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
        When the signed-in user tries to access the registration or login page
        Then the system should prevent access and redirect the user to the homepage or dashboard

    Scenario: Non-signed-in user attempts to access signed-in user pages without authentication
        Given a non-signed-in user is not logged into the system
        When the non-signed-in user tries to access signed-in user pages directly via URL
        Then the system should prevent access and redirect the user to the login page

    Scenario: Signed-in user successfully accesses signed-in user pages
        Given a user is signed in with UserName "testuser@fakemail.com" and Password "Password123!"
        When the signed-in user tries to access signed-in user pages
        Then the system should allow access without any redirection to the signed-in user pages

    Scenario: Non-signed-in user successfully accesses non-signed-in user pages
        Given a non-signed-in user is not logged into the system
        When the non-signed-in user tries to access non-signed-in user pages
        Then the system should allow access without any redirection
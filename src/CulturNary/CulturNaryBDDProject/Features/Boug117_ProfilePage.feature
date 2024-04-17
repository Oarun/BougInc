# Feature: User Profiles (Boug-117)

# Logged in users should be able to view their own profile page, which should contain their display name, biography, and profile picture. 

# They should also be able to reach the edit pages for their display name, biography, and profile picture from this page, and see any changes made be reflected.

#     Background:
#         Given the following user exists
#         | UserName | Password | DisplayName | Biography | ProfileImageName |
#         | testuser@fakemail.com | Password123! | Test User    | Hello!    | scrungle.jpg |

#     Scenario: Profile Page contains existing user information
#         Given a user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
#         When the user navigates to the Profile page
#         Then the user should see their display name, biography, and profile picture
#         And they should see an button to edit each of these

#     Scenario: User updates their display name
#         Given a user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
#         And the user navigates to the Profile page
#         And clicks the button to edit their display name
#         When the user enters a new display name on the edit page
#         And clicks the button to save their new display name
#         Then the user should see the updated display name on their profile page

#     Scenario: User updates their biography
#         Given a user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
#         And the user navigates to the Profile page
#         And clicks the button to edit their biography
#         When the user enters a new biography on the edit page
#         And clicks the button to save their new biography
#         Then the user should see the updated biopgraphy on their profile page

#     Scenario: User updates their profile picture
#         Given a user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
#         And the user navigates to the Profile page
#         And clicks the button to edit their profile picture
#         When the user selects a new picture on the edit page
#         And clicks the button to upload their new picture
#         Then the user should see the new profile picture on their profile page
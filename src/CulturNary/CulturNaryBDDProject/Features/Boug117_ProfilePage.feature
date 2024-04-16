# @Lunafreyja
# Feature: User Profiles (Boug-117)

# Logged in users should be able to view their own profile page, which should contain their display name, biography, and profile picture. 

# They should also be able to reach the edit pages for their display name, biography, and profile picture from this page, and see any changes made be reflected.

# Background:
#     Given the following user exists
#         | UserName | Password | DisplayName | Biography | ProfileImageName |
#         | testuser@fakemail.com | password123 | Test User    | Hello!    | scrungle.jpg |
#     And I login as 'testuser@fakemail.com' with password 'password123'

# Scenario: Profile Page contains existing user information

#     Given I am a logged in user
#     When I go to the user profile page
#     Then I should see my display name, biography, and profile picture
#     And I should see an button to edit each of these

# Scenario: User updates their display name
#     Given a logged-in user is on their profile page
#     And clicks the button to edit their display name
#     When the user updates their information on the edit page
#     And clicks the save button
#     Then it should save the changes and display a confirmation message 
#     And it should handle errors and display clear error messages if the update fails

# Scenario: User updates their biography
#     Given a logged-in user is on their profile page
#     And clicks the button to edit their biography
#     When the user updates their information on the edit page
#     And clicks the save button
#     Then it should save the changes and display a confirmation message 
#     And it should handle errors and display clear error messages if the update fails

# Scenario: User updates their profile picture
#     Given a logged-in user is on their profile page
#     And clicks the button to edit their profile picture
#     When the user updates their information on the edit page
#     And clicks the save button
#     Then it should save the changes and display a confirmation message 
#     And it should handle errors and display clear error messages if the update fails
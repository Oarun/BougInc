Feature: Boug215_NewsPage

As a user, I want to access a dedicated page in the app where I can browse and read a variety of articles related to food and nutrition news so that I can stay informed about the latest trends, research, and tips in nutrition.

Background:
        Given the following user exists in BougTwoFifteen
        | UserName | Password | DisplayName | Biography | ProfileImageName |
        | testuser@fakemail.com | Password123! | Test User    | Hello!    | scrungle.jpg |

@news
Scenario: As a logged in user I want to access the news page
	Given the user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
	When the user navigates to the News page
	Then the user should see a page with the title 'Food & Nutrition News Page - CulturNary'
	And they should see an empty page with a form of select options
	And they should see a button to submit the form

@news
Scenario: As a logged in user I want to search a category in the news page
	Given the user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
	And the user is on the news page
	When the user selects a category from the form
	Then the user should see a list of articles related to the category
	And they should see a button to view the full article

@news
Scenario: As a logged user I want to view a full article from its source
	Given the user is logged in with the UserName 'testuser@fakemail.com' and Password 'Password123!'
	And the user has made a category search on the news page
	When the user selects the read more link on an article
	Then the user should be redirected to the source of the article in a new tab

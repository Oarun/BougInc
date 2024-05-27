Feature: User Profiles (Boug-247)

    Stuff.

    Scenario: User Changes Brightness of Site
        Given I am on the home page
        When I change the brightness of the site to Light Mode
        Then the site should change theme to Light Mode 

    Scenario: User Changes Color of Site
        Given I am on the home page
        When I change the color of the site to Red
        Then the site should change theme to Red

    Scenario: User Changes Color and Brightness of Site
        Given I am on the home page
        When I change the color of the site to Blue
        And I change the brightness of the site to Dark Mode
        Then the site should change theme to Dark Mode and Blue
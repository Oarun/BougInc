Feature: Sharing a favorite recipe

  Background:
    Given the following users exist in BougTwoFourtyFour
        | UserName             | Password    |
        | UserA@fakemail.com   | Password123!|
        | UserB@fakemail.com   | Password123!|

  Scenario: UserA shares a favorite recipe with UserB
    Given UserA is logged into their account
    And UserA has marked a recipe as a favorite
    When UserA selects the option to share the favorite recipe with UserB
    Then UserB should receive it

  Scenario: UserA confirms the shared recipe
    Given UserA has shared a favorite recipe with UserB
    When UserB views their shared recipes list
    Then the shared recipe should be listed there
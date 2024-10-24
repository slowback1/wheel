Feature: Manage wheels to spin at a later time

    Scenario: User creates a wheel
        Given I intend to create a wheel
        When I create a wheel
        Then The wheel should be created

    Scenario: User accesses a wheel that they created
        Given I intend to create a wheel
        When I create a wheel
        Then I should be able to access the wheel

    Scenario: User views previously created wheels
        Given I have created two wheels
        When I view my wheels
        Then I should see two wheels

    Scenario: User tries to access a wheel that does not exist
        Given I have created a wheel
        When I try to access a wheel that does not exist
        Then I should be notified that the wheel does not exist

    Scenario: User tries to create a wheel that already exists
        Given I have created a wheel
        When I try to create a wheel with the same name
        Then I should be notified that the wheel already exists

    Scenario: User is notified when an error occurs when getting wheels
        Given I have created a wheel
        When I try to get my wheels and an error occurs
        Then I should be notified that an error occurred

    Scenario: Message Bus broadcasts wheel creation
        Given I intend to create a wheel
        When I create a wheel
        Then The message bus should broadcast the wheel creation

    @ignore
    @not-implemented
    Scenario: User edits a wheel
        Given I have created a wheel
        When I edit the wheel
        Then The wheel should be updated
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

    @ignore
    @not-implemented
    Scenario: User edits a wheel
        Given I have created a wheel
        When I edit the wheel
        Then The wheel should be updated
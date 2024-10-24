Feature: Spin wheels to determine a random result

    @ignore
    @not-implemented
    Scenario: User spins a wheel
        Given I have created a wheel with "Red", "Green", "Blue"
        When I spin the wheel
        Then The wheel should land on "Red" or "Green" or "Blue"

    @ignore
    @not-implemented
    Scenario: User spins a rigged wheel
        Given I have created a wheel with "Red", "Green", "Blue"
        And I have rigged the wheel to land on "Red"
        When I spin the wheel
        Then The wheel should land on "Red"

    @ignore
    @not-implemented
    Scenario: User spins a wheel multiple times
        Given I have created a wheel with "Red", "Green", "Blue"
        When I spin the wheel 10 times
        Then The wheel should land on "Red" or "Green" or "Blue" 10 times

    @ignore
    @not-implemented
    Scenario: User spins a wheel multiple times in "distribution" mode
        Given I have created a wheel with "Red", "Green", "Blue"
        When I spin the wheel 30 times in "equal distribution" mode
        Then The wheel should land on "Red" or "Green" or "Blue" 10 times each

    @ignore
    @not-implemented
    Scenario: User spins a wheel multiple in "distribution" mode, with one option having a 50% chance
        Given I have created a wheel with "Red", "Green", "Blue"
        And I have set the wheel to have a 50% chance of landing on "Red"
        When I spin the wheel 10 times in "distribution" mode
        Then The wheel should land on "Red" 5 times
        And The wheel should land on "Green" or "Blue" 5 times
Feature: Spin wheels to determine a random result

    Scenario: User spins a wheel
        Given I have a wheel with "Red", "Green", "Blue"
        When I spin the wheel
        Then The wheel should land on one of the slices

    @ignore
    @not-implemented
    Scenario: User spins a rigged wheel
        Given I have a wheel with "Red", "Green", "Blue"
        And I have rigged the wheel to land on "Red"
        When I spin the wheel
        Then The wheel should land on "Red"

    Scenario: User spins a wheel multiple times
        Given I have a wheel with "Red", "Green", "Blue"
        When I spin the wheel "10" times
        Then The wheel should land on "Red" or "Green" or "Blue" "10" times

    @ignore
    @not-implemented
    Scenario: User spins a wheel multiple times in "distribution" mode
        Given I have a wheel with "Red", "Green", "Blue"
        When I spin the wheel 30 times in "equal distribution" mode
        Then The wheel should land on "Red" or "Green" or "Blue" "10" times each

    @ignore
    @not-implemented
    Scenario: User spins a wheel multiple in "distribution" mode, with one option having a 50% chance
        Given I have a wheel with "Red", "Green", "Blue" with "Red" having a 50% chance
        When I spin the wheel 10 times in "distribution" mode
        Then The wheel should land on "Red" 5 times
        And The wheel should land on 'Green' or 'Blue' 5 times
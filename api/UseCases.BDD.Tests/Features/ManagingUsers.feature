Feature: Managing user data

    @ignore
    @not-implemented
    Scenario: Registering as a new user
        Given I want to start storing wheel data between sessions
        When I register as a new user
        Then I should be able to access my own wheels

    @ignore
    @not-implemented
    Scenario: Registering as a new user when the username already exists fails
        Given I want to start storing wheel data between sessions
        And another user already exists with the same username
        When I register as a new user
        Then I should be notified that the username already exists

    @ignore
    @not-implemented
    Scenario: Registering as a new user with an invalid password fails
        Given I want to start storing wheel data between sessions
        When I register as a new user with an invalid password
        Then I should be notified that the password is invalid

    @ignore
    @not-implemented
    Scenario: Registering as a new user with an invalid username fails
        Given I want to start storing wheel data between sessions
        When I register as a new user with an invalid username
        Then I should be notified that the username is invalid

    @ignore
    @not-implemented
    Scenario: Registrying as a new user stores the password securely
        Given I want to start storing wheel data between sessions
        When I register as a new user
        Then My password should be stored securely

    @ignore
    @not-implemented
    Scenario: User logs in
        Given I have registered as a new user
        When I log in
        Then I should be able to access my own wheels

    @ignore
    @not-implemented
    Scenario: Trying to log in with an invalid username
        Given I have registered as a new user
        When I try to log in with an invalid username
        Then I should be notified that the username or password is invalid

    @ignore
    @not-implemented
    Scenario: Trying to log in with an invalid password
        Given I have registered as a new user
        When I try to log in with an invalid password
        Then I should be notified that the username or password is invalid

    @ignore
    @not-implemented
    Scenario: Deleting a user account deletes the user
        Given I have registered as a new user
        When I delete my user account
        Then I should no longer be able to access my wheels

    @ignore
    @not-implemented
    Scenario: Deleting a user account deletes the user's wheels
        Given I have registered as a new user
        And I have created a wheel
        When I delete my user account
        Then My wheels should no longer be stored
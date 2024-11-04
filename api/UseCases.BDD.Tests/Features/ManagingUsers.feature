Feature: Managing user data

    Scenario: Registering as a new user
        Given I want to start storing wheel data between sessions
        When I register as a new user
        Then I should be able to access my own wheels

    Scenario: Registering as a new user when the username already exists fails
        Given I want to start storing wheel data between sessions
        And another user already exists with the same username
        When I register as a new user
        Then I should be notified that the username already exists

    Scenario Outline: Registering as a new user with an invalid password fails
        Given I want to start storing wheel data between sessions
        When I register as a new user with an invalid password <Password> which is invalid
        Then I should be notified that the password is invalid because <Reason> as the reason

        Examples:
          | Password              | Reason                                       |
          | ""                    | it is too short                              |
          | noNumbers!            | it does not contain any numbers              |
          | 12345678!             | it does not contain any letters              |
          | NoSpecialCharacters11 | it does not contain any special characters   |
          | nouppercaseletters!1  | it does not contain any uppercase characters |
          | NOLOWERCASELETTERS!1  | it does not contain any lowercase characters |

    Scenario Outline: Registering as a new user with an invalid username fails
        Given I want to start storing wheel data between sessions
        When I register as a new user with username <Username> which is invalid
        Then I should be notified that the username is invalid because <Reason>

        Examples:
          | Username | Reason          |
          | a        | it is too short |

    Scenario: Registrying as a new user stores the password securely
        Given I want to start storing wheel data between sessions
        When I register as a new user
        Then My password should be stored securely

    Scenario: User logs in
        Given I have registered as a new user
        When I log in
        Then I should be able to access my own wheels

    Scenario: Trying to log in with an invalid username
        Given I have registered as a new user
        When I try to log in with an invalid username
        Then I should be notified that the username or password is invalid

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
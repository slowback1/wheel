using Dsl;
using TechTalk.SpecFlow;

namespace UseCases.BDD.Tests.StepDefinitions;

[Binding]
public class ManagingUsersSteps
{
    private ManagingUsersDsl Feature { get; set; }

    [Given(@"I want to start storing wheel data between sessions")]
    public void GivenIWantToStartStoringWheelDataBetweenSessions()
    {
        Feature = new ManagingUsersNotCreatedYetDsl();
    }

    [When(@"I register as a new user")]
    public async Task WhenIRegisterAsANewUser()
    {
        await Feature.Register();
    }

    [Then(@"I should be able to access my own wheels")]
    public void ThenIShouldBeAbleToAccessMyOwnWheels()
    {
        Feature.AssertIsLoggedIn();
    }

    [Given(@"another user already exists with the same username")]
    public async Task GivenAnotherUserAlreadyExistsWithTheSameUsername()
    {
        await Feature.Register();
    }

    [Then(@"I should be notified that the username already exists")]
    public void ThenIShouldBeNotifiedThatTheUsernameAlreadyExists()
    {
        Feature.AssertLastErrorIs("already exists");
    }

    [When(@"I register as a new user with an invalid password (.*) which is invalid")]
    public async Task WhenIRegisterAsANewUserWithAnInvalidPassword(string password)
    {
        await Feature.Register(password: password);
    }

    [Then(@"I should be notified that the password is invalid because (.*) as the reason")]
    public void ThenIShouldBeNotifiedThatThePasswordIsInvalidBecause(string message)
    {
        Feature.AssertLastErrorIs(message);
    }

    [When(@"I register as a new user with username (.*) which is invalid")]
    public async Task WhenIRegisterAsANewUserWithAnInvalidUsername(string username)
    {
        await Feature.Register(username);
    }

    [Then(@"I should be notified that the username is invalid because (.*)")]
    public void ThenIShouldBeNotifiedThatTheUsernameIsInvalidBecause(string message)
    {
        Feature.AssertLastErrorIs(message);
    }
}
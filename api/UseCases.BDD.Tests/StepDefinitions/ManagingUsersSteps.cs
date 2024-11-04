using Data.InMemory;
using TechTalk.SpecFlow;
using UseCases.BDD.Tests.Dsl.Users;

namespace UseCases.BDD.Tests.StepDefinitions;

[Binding]
public class ManagingUsersSteps
{
    private ManagingUsersDsl Feature { get; set; }

    [Before]
    public void Before()
    {
        InMemoryStore.Users.Clear();
    }

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

    [Then(@"My password should be stored securely")]
    public void ThenMyPasswordShouldBeStoredSecurely()
    {
        Feature.AssertPasswordIsStoredSecurely();
    }

    [Given(@"I have registered as a new user")]
    public async Task GivenIHaveRegisteredAsANewUser()
    {
        Feature = await ManagingUserAlreadyRegisteredDsl.Create();
    }

    [When(@"I log in")]
    public async Task WhenILogIn()
    {
        await Feature.Login();
    }

    [When(@"I try to log in with an invalid username")]
    public async Task WhenITryToLogInWithAnInvalidUsername()
    {
        await Feature.Login("not my username");
    }

    [Then(@"I should be notified that the username or password is invalid")]
    public void ThenIShouldBeNotifiedThatTheUsernameOrPasswordIsInvalid()
    {
        Feature.AssertLastErrorIs("Invalid username or password");
    }

    [When(@"I try to log in with an invalid password")]
    public async Task WhenITryToLogInWithAnInvalidPassword()
    {
        await Feature.Login(password: "not my password");
    }
}
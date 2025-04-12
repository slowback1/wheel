using Data.File;
using Data.File.Store;
using Data.Tests.File.Store;
using TestUtilities.TestData;

namespace Data.Tests.File;

public class FileWheelRetrieverTests
{
    private const string NonExistingWheelId = "NonExistingWheelId";
    private const string NonExistingUsername = "NonExistingUsername";
    private IFileStoreRetriever FileStoreRetriever { get; set; }
    private string WheelId { get; set; }
    private string Username { get; set; }


    [SetUp]
    public async Task Setup()
    {
        FileStoreRetriever = new TestFileStoreRetriever();

        var wheel = TestWheelSettings.GetTestWheelSetting();

        var creator = new FileWheelCreator(FileStoreRetriever);

        var createdWheel = await creator.CreateWheelSetting(wheel);

        WheelId = createdWheel.SavedEntity!.Name;
        Username = wheel.Username;
    }

    [Test]
    public async Task GetWheelSetting_WhenCalled_ReturnsWheelSetting()
    {
        var retriever = new FileWheelRetriever(FileStoreRetriever);

        var result = await retriever.GetWheelSetting(WheelId);

        Assert.That(result, Is.Not.Null);

        Assert.That(result!.Name, Is.EqualTo(WheelId));
    }

    [Test]
    public async Task GetWheelSetting_WhenWheelDoesNotExist_ReturnsNull()
    {
        var retriever = new FileWheelRetriever(FileStoreRetriever);

        var result = await retriever.GetWheelSetting(NonExistingWheelId);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetWheelSettings_ForUser_ReturnsWheelSettings()
    {
        var retriever = new FileWheelRetriever(FileStoreRetriever);

        var result = await retriever.GetWheelSettings(Username);

        Assert.That(result, Is.Not.Null);

        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public async Task GetWheelSettings_ForUserWithNoWheels_ReturnsEmptyList()
    {
        var retriever = new FileWheelRetriever(FileStoreRetriever);

        var result = await retriever.GetWheelSettings(NonExistingUsername);

        Assert.That(result, Is.Not.Null);

        Assert.That(result.Count(), Is.EqualTo(0));
    }
}
using Common.Data;
using Data.File;
using Data.File.Store;
using Data.Tests.File.Store;
using TestUtilities.TestData;

namespace Data.Tests.File;

public class FileWheelUpdaterTests
{
    private IFileStoreRetriever FileStoreRetriever { get; set; }
    private string WheelId { get; set; }


    [SetUp]
    public async Task Setup()
    {
        FileStoreRetriever = new TestFileStoreRetriever();

        var wheel = TestWheelSettings.GetTestWheelSetting();

        var creator = new FileWheelCreator(FileStoreRetriever);

        var createdWheel = await creator.CreateWheelSetting(wheel);

        WheelId = createdWheel.SavedEntity!.Name;
    }

    [Test]
    public async Task CanUpdateWheelSetting()
    {
        var wheel = TestWheelSettings.GetTestWheelSetting();

        wheel.Name = WheelId;
        wheel.Slices = new List<WheelSlice>
        {
            new()
            {
                Label = "updated 1"
            },
            new()
            {
                Label = "updated 2"
            }
        };

        var updater = new FileWheelUpdater(FileStoreRetriever);

        await updater.UpdateWheelSetting(wheel);

        var retriever = new FileWheelRetriever(FileStoreRetriever);

        var result = await retriever.GetWheelSetting(WheelId);

        Assert.That(result, Is.Not.Null);

        Assert.That(result!.Name, Is.EqualTo(WheelId));

        Assert.That(result.Slices.Count(), Is.EqualTo(2));
        Assert.That(result.Slices.First().Label, Is.EqualTo("updated 1"));
        Assert.That(result.Slices.Last().Label, Is.EqualTo("updated 2"));
    }
}
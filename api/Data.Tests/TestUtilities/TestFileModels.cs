using Data.File.Models;

namespace Data.Tests.TestUtilities;

internal static class TestFileModels
{
    public static FileUser GetTestFileUser()
    {
        return new FileUser
        {
            Username = "TestUsername",
            PasswordHash = "TestPasswordHash"
        };
    }

    public static FileWheelSlice GetTestFileWheelSlice()
    {
        return new FileWheelSlice
        {
            Label = "Label",
            Size = 1
        };
    }

    public static FileWheel GetTestFileWheel()
    {
        return new FileWheel
        {
            Name = "TestWheel",
            Slices = new List<FileWheelSlice>
            {
                GetTestFileWheelSlice()
            }
        };
    }
}
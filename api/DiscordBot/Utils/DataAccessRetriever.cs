using Common.Interfaces;
using Data.File;
using Data.File.Store;

namespace DiscordBot.Utils;

public static class DataAccessRetriever
{
    private const string FilePath = "Data/db.json";

    public static IDataAccess GetDataAccess()
    {
        return new FileDataAccess(new FileStorageSettings(FilePath));
    }
}
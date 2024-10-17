namespace Common.Data;

public class SaveResult<T>
{
    private SaveResult()
    {
    }

    public T? SavedEntity { get; set; }
    public bool SaveSuccessful { get; set; }
    public string? ErrorMessage { get; set; }

    public static SaveResult<T> Success(T savedEntity)
    {
        return new SaveResult<T>
        {
            SavedEntity = savedEntity,
            SaveSuccessful = true
        };
    }

    public static SaveResult<T> Failure(string? errorMessage)
    {
        return new SaveResult<T>
        {
            SaveSuccessful = false,
            ErrorMessage = errorMessage
        };
    }
}
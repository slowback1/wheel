using System;

namespace Common.Data;

public class FeatureResult<T>
{
    private FeatureResult()
    {
    }

    public T? Data { get; set; }
    public FeatureResultStatus Status { get; set; }
    public Exception? Exception { get; set; }

    public static FeatureResult<T> Ok(T data)
    {
        return new FeatureResult<T>
        {
            Data = data,
            Status = FeatureResultStatus.Ok
        };
    }

    public static FeatureResult<T> NotFound()
    {
        return new FeatureResult<T>
        {
            Status = FeatureResultStatus.NotFound
        };
    }

    public static FeatureResult<T> Error(Exception? exception = null)
    {
        return new FeatureResult<T>
        {
            Status = FeatureResultStatus.Error,
            Exception = exception
        };
    }

    public static FeatureResult<T> Error(string message)
    {
        return new FeatureResult<T>
        {
            Status = FeatureResultStatus.Error,
            Exception = new Exception(message)
        };
    }
}

public enum FeatureResultStatus
{
    Ok,
    NotFound,
    Error
}
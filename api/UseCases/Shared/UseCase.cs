using System;
using System.Threading.Tasks;
using Common.Data;

namespace UseCases.Shared;

public abstract class UseCase
{
    protected async Task<FeatureResult<T>> Execute<T>(Func<Task<FeatureResult<T>>> action)
    {
        try
        {
            var result = await action();

            return result;
        }
        catch (Exception e)
        {
            return FeatureResult<T>.Error(e);
        }
    }
}
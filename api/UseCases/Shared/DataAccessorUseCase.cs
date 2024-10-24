using Common.Interfaces;

namespace UseCases.Shared;

public abstract class DataAccessorUseCase : UseCase
{
    protected readonly IDataAccess _dataAccess;

    protected DataAccessorUseCase(IDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }
}
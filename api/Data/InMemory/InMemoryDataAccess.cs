﻿using Common.Interfaces;

namespace Data.InMemory;

public class InMemoryDataAccess : IDataAccess
{
    public IWheelCreator WheelCreator { get; } = new InMemoryWheelCreator();
    public IWheelRetriever WheelRetriever { get; } = new InMemoryWheelRetriever();
    public IUserCreator UserCreator { get; } = new InMemoryUserCreator();
    public IUserRetriever UserRetriever { get; } = new InMemoryUserRetriever();
}
﻿using Common.Interfaces;

namespace TestUtilities.MockImplementations;

public class TestDataAccess : IDataAccess
{
    public IWheelCreator WheelCreator { get; } = new TestWheelCreator();
    public IWheelRetriever WheelRetriever { get; } = new TestWheelRetriever();
    public IUserCreator UserCreator { get; } = new TestUserCreator();
    public IUserRetriever UserRetriever { get; } = new TestUserRetriever();
    public IWheelUpdater WheelUpdater { get; } = new TestWheelUpdater();
    public IWheelDeleter WheelDeleter { get; } = new TestWheelDeleter();
}
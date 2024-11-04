﻿using Common.Data;
using Common.Interfaces;
using Data.InMemory;
using Infrastructure.Cryptography;
using Infrastructure.Messaging;
using UseCases.User;

namespace UseCases.BDD.Tests.Dsl.Users;

public abstract class ManagingUsersDsl
{
    private const string DefaultUsername = "Username";
    private const string DefaultPassword = "Password!1";

    protected ManagingUsersDsl()
    {
        SendHashingOptionsToTheMessageBus();
    }

    private string CurrentLoggedInHash { get; set; } = string.Empty;
    private Exception? LastError { get; set; }
    protected IDataAccess DataAccess { get; set; } = new InMemoryDataAccess();

    public async Task Register(string username = DefaultUsername, string password = DefaultPassword)
    {
        var user = new CreateUser
        {
            Password = password,
            Username = username
        };

        var useCase = new RegisterUserUseCase(DataAccess);

        var result = await useCase.Register(user);

        if (result.Status == FeatureResultStatus.Ok)
            CurrentLoggedInHash = result.Data!;
        if (result.Status == FeatureResultStatus.Error)
            LastError = result.Exception;
    }

    public void AssertIsLoggedIn()
    {
        Assert.That(CurrentLoggedInHash, Is.Not.Empty);
    }

    public void AssertLastErrorIs(string message)
    {
        Assert.That(LastError, Is.Not.Null);
        Assert.That(LastError!.Message, Contains.Substring(message));
    }

    public void AssertPasswordIsStoredSecurely()
    {
        var storedUser = InMemoryStore.Users.Find(u => u.Username == DefaultUsername);

        var password = storedUser?.PasswordHash;

        Assert.That(password, Is.Not.Null);
        Assert.That(password, Is.Not.EqualTo(DefaultPassword));
    }

    public async Task Login(string username = DefaultUsername, string password = DefaultPassword)
    {
        var useCase = new LoginUseCase(DataAccess);

        var result = await useCase.Login(username, password);

        if (result.Status == FeatureResultStatus.Ok)
            CurrentLoggedInHash = result.Data!;
        if (result.Status == FeatureResultStatus.Error)
            LastError = result.Exception;
    }

    private void SendHashingOptionsToTheMessageBus()
    {
        var options = new HashingOptions
        {
            HashPrefix = "test-hash",
            HashSize = 8,
            SaltSize = 8
        };

        MessageBus.Publish(Messages.HashingOptions, options);
    }
}
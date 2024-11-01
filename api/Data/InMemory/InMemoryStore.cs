using System.Collections.Generic;
using Common.Data;
using Data.InMemory.Models;

namespace Data.InMemory;

internal static class InMemoryStore
{
    public static readonly List<WheelSetting> Wheels = new();
    public static readonly List<InMemoryUser> Users = new();
}
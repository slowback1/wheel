using System.Linq;
using Common.Data;
using Data.File.Models;

namespace Data.File.Mappers;

internal static class FileWheelMappers
{
    public static FileWheel ToFileWheel(this WheelSetting wheelSetting)
    {
        return new FileWheel
        {
            Name = wheelSetting.Name,
            Slices = wheelSetting.Slices
                .Select(ToFileWheelSlice)
                .ToList()
        };
    }

    public static WheelSetting ToWheelSetting(this FileWheel fileWheel)
    {
        return new WheelSetting
        {
            Name = fileWheel.Name,
            Slices = fileWheel
                .Slices
                .Select(ToWheelSlice)
                .ToList()
        };
    }

    public static FileWheel ToFileWheel(this CreateWheelSetting wheel)
    {
        return new FileWheel
        {
            Name = wheel.Name,
            Username = wheel.Username,
            Slices = wheel
                .Slices
                .Select(ToFileWheelSlice)
                .ToList()
        };
    }

    public static WheelSlice ToWheelSlice(this FileWheelSlice fileWheelSlice)
    {
        return new WheelSlice
        {
            Label = fileWheelSlice.Label,
            Size = fileWheelSlice.Size
        };
    }

    public static FileWheelSlice ToFileWheelSlice(this WheelSlice wheelSlice)
    {
        return new FileWheelSlice
        {
            Label = wheelSlice.Label,
            Size = wheelSlice.Size
        };
    }
}
﻿using System;

namespace Infrastructure.Time;

public interface ITimeProvider
{
    DateTime Now();
    DateTime Today();
    DateTime FromString(string value);
    DateTime UtcNow();
    DateTime AddDays(DateTime date, int days);
    DateTime AddHours(DateTime date, int hours);
    DateTime AddMinutes(DateTime date, int minutes);
    DateTime AddSeconds(DateTime date, int seconds);
}
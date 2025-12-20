# ScheduledJobs Library

This library provides scheduled job functionality for the Wheel application, specifically for running preset intervals at regular frequencies.

## IntervalScheduler

The `IntervalScheduler` class is responsible for:
- Checking every hour for intervals that are due to be executed
- Executing intervals by spinning their associated presets
- Updating interval execution times after successful runs

### Usage

```csharp
var dataAccess = // Get IDataAccess implementation
var scheduler = new IntervalScheduler(dataAccess);
scheduler.Start();
// ... scheduler runs in the background
scheduler.Stop(); // When shutting down
```

## How It Works

1. The scheduler runs a Timer that checks every hour for due intervals
2. For each due interval:
   - Retrieves the associated preset
   - Spins the wheel using the preset
   - Logs the result to console
   - Updates the interval's last run time and calculates the next run time
3. All operations are wrapped in exception handling to prevent crashes

## Integration with Discord Bot

The scheduler is initialized in the Discord bot's `Program.cs` and runs continuously in the background. It does not send Discord messages directly - interval results are only logged to the console.

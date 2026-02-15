namespace StandUpTimer.Core.Models;

public record AppState
{
    public DateTime StartTime { get; init; }
    public DateTime LastNotificationTime { get; init; }
    public bool IsRunning { get; init; }
    
    public static AppState Initial() => new()
    {
        StartTime = DateTime.Now,
        LastNotificationTime = DateTime.Now,
        IsRunning = true
    };
}
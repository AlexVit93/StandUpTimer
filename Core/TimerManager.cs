using StandUpTimer.Core.Models;

namespace StandUpTimer.Core;

public class TimerManager : IDisposable
{
    private readonly System.Threading.Timer _timer;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(60);
    private AppState _currentState;
    
    public event Action<AppState>? TimerElapsed;
    public event Action<AppState>? StateChanged;
    
    public TimerManager()
    {
        _currentState = AppState.Initial();
        _timer = new System.Threading.Timer(
            OnTimerTick, 
            null, 
            _interval, 
            _interval);
    }
    
    private void OnTimerTick(object? state)
    {
        var newState = _currentState with
        {
            LastNotificationTime = DateTime.Now
        };
        
        UpdateState(newState);
        TimerElapsed?.Invoke(newState);
    }
    
    public void ResetTimer()
    {
        var newState = AppState.Initial();
        UpdateState(newState);
        _timer.Change(_interval, _interval);
    }
    
    private void UpdateState(AppState newState)
    {
        _currentState = newState;
        StateChanged?.Invoke(_currentState);
    }
    
    public void Dispose() => _timer?.Dispose();
}
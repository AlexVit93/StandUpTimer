using StandUpTimer.Core;
using StandUpTimer.Core.Models;
using StandUpTimer.UI;

namespace StandUpTimer;

public class AppController : IDisposable
{
    private readonly TimerManager _timerManager;
    private readonly NotificationService _notificationService;
    private readonly TrayIcon _trayIcon;
    private AppState _currentState;
    
    public AppController()
    {
        _timerManager = new TimerManager();
        _notificationService = new NotificationService();
        _trayIcon = new TrayIcon();
        
        _timerManager.TimerElapsed += OnTimerElapsed;
        _timerManager.StateChanged += OnStateChanged;
        _trayIcon.ResetTimerRequested += OnResetTimerRequested;
        _trayIcon.ExitRequested += OnExitRequested;
        
        _currentState = AppState.Initial();
    }
    
private void OnTimerElapsed(AppState state)
{
    var form = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;
    
    if (form != null)
    {
        form.BeginInvoke(() =>
        {
            _notificationService.ShowNotification();
            _trayIcon.ShowNotification("90 минут истекло! Пора размяться.");
        });
    }
    else
    {
        _notificationService.ShowNotification();
        _trayIcon.ShowNotification("90 минут истекло! Пора размяться.");
    }
}
    
    private void OnStateChanged(AppState state)
    {
        _currentState = state;
    }
    
    private void OnResetTimerRequested()
    {
        _timerManager.ResetTimer();
        _notificationService.HideNotification();
        _trayIcon.ShowNotification("Таймер сброшен. Следующее напоминание через 90 минут.");
    }
    
    private void OnExitRequested()
    {
        Application.Exit();
    }
    
    public void Run()
{
    var dummyForm = new Form 
    { 
        WindowState = FormWindowState.Minimized, 
        ShowInTaskbar = false,
        FormBorderStyle = FormBorderStyle.None
    };   
    dummyForm.Load += (s, e) => dummyForm.Hide();
    Application.Run(dummyForm);
}
    
    public void Dispose()
    {
        _timerManager.Dispose();
        _trayIcon.Dispose();
    }
}




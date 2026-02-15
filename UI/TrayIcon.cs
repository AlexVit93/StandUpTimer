namespace StandUpTimer.UI;

public class TrayIcon : IDisposable
{
    private readonly NotifyIcon _notifyIcon;
    private readonly ContextMenuStrip _contextMenu;
    
    public event Action? ResetTimerRequested;
    public event Action? ExitRequested;
    
    public TrayIcon()
    {
        _notifyIcon = new NotifyIcon
        {
            Icon = new Icon("Resources/timer.ico"),
            Text = "StandUp Timer\nКаждые 90 минут",
            Visible = true
        };
        
        _contextMenu = new ContextMenuStrip();
        _contextMenu.Items.Add("Сбросить таймер", null, (s, e) => ResetTimerRequested?.Invoke());
        _contextMenu.Items.Add("-");
        _contextMenu.Items.Add("Выход", null, (s, e) => ExitRequested?.Invoke());
        
        _notifyIcon.ContextMenuStrip = _contextMenu;
        _notifyIcon.MouseClick += OnMouseClick;
    }
    
    private void OnMouseClick(object? sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            _contextMenu.Show(Cursor.Position);
        }
    }
    
    public void ShowNotification(string text)
    {
        _notifyIcon.ShowBalloonTip(3000, "StandUp Timer", text, ToolTipIcon.Info);
    }
    
    public void Dispose()
    {
        _notifyIcon.Visible = false;
        _notifyIcon.Dispose();
        _contextMenu.Dispose();
    }
}
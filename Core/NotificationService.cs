namespace StandUpTimer.Core;

public class NotificationService
{
    private Form? _activeForm;
    
    public void ShowNotification()
    {
        _activeForm?.Close();
        
        _activeForm = new Form
        {
            Text = "Пора встать!",
            Size = new Size(600, 400),
            StartPosition = FormStartPosition.CenterScreen,
            TopMost = true,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            ControlBox = true,
            MinimizeBox = false,
            MaximizeBox = false,
            WindowState = FormWindowState.Normal
        };
        
        var label = new Label
        {
            Text = "ВСТАНЬ И ПРОЙДИСЬ!\n\nПосидел 60 минут - пора размяться.",
            Font = new Font("Arial", 24, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };
        
        var closeButton = new Button
        {
            Text = "Я встал",
            Size = new Size(200, 50),
            Location = new Point(200, 300)
        };
        
        closeButton.Click += (s, e) => _activeForm?.Close();
        
        _activeForm.Controls.AddRange(new Control[] { label, closeButton });
        _activeForm.FormClosed += (s, e) => _activeForm = null;
        
        _activeForm.Show();
    }
    
    public void HideNotification() => _activeForm?.Close();
}

public class TrayApplicationContext : ApplicationContext
{
    private readonly NotifyIcon _trayIcon;

    public TrayApplicationContext()
    {
        var contextMenu = new ContextMenuStrip();
        contextMenu.Items.Add("Status: Running", null).Enabled = false;
        contextMenu.Items.Add(new ToolStripSeparator());
        contextMenu.Items.Add("Exit", null, OnExit);

        _trayIcon = new NotifyIcon
        {
            Icon = SystemIcons.Information,
            ContextMenuStrip = contextMenu,
            Text = "TrueNAS MCP",
            Visible = true
        };

        _trayIcon.ShowBalloonTip(3000, "TrueNAS MCP", "Running in tray", ToolTipIcon.Info);
    }

    private void OnExit(object? sender, EventArgs e)
    {
        _trayIcon.Visible = false;
        Application.Exit();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _trayIcon.Dispose();
        base.Dispose(disposing);
    }
}

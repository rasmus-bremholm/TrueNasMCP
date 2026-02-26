using System.Drawing.Text;
namespace TrueNasMCP;

public class TrayApplicationContext : ApplicationContext
{
   private readonly NotifyIcon _trayIcon;

   public TrayApplicationContext()
   {
      var contextMenu = new ContextMenuStrip();
      contextMenu.Items.Add("Status: Starting...", null).Enabled = false;
      contextMenu.Items.Add(new ToolStripSeparator());
      contextMenu.Items.Add("Quit", null, OnExit);

      // Just the tray icon config.
      _trayIcon = new NotifyIcon
      {
         Icon = SystemIcons.Information, // swap for real icon later
         ContextMenuStrip = contextMenu,
         Text = "TrueNAS MCP",
         Visible = true
      };


      // BalloonTip makes a little cute windows snackbar.
      _trayIcon.ShowBalloonTip(
         timeout: 3000,
         tipTitle: "TrueNAS MCP",
         tipText: "MCP server is starting...",
         tipIcon: ToolTipIcon.Info
      );
   }

   private void OnExit(object? sender, EventArgs e)
   {
      _trayIcon.Visible = false;
      Application.Exit();
   }

   protected override void Dispose(bool disposing)
   {
      if(disposing)
      {
         _trayIcon.Dispose();
      }
      base.Dispose(disposing);
   }
}

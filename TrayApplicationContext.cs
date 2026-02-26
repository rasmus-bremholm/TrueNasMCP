namespace TrueNasMCP;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TrueNasMCP.Mcp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

public class TrayApplicationContext : ApplicationContext
{
   private readonly NotifyIcon _trayIcon;
   private readonly IHost _host;
   private readonly CancellationTokenSource _cst = new();

   public TrayApplicationContext()
   {
      // Its like a small Asp.Net app living inside my app to handle HTTP requests.
      _host = CreateHost();

      Task.Run(() => _host.RunAsync(_cst.Token));



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

   private static IHost CreateHost()
   {
      var builder = WebApplication.CreateBuilder();

      builder.Services
      .AddMcpServer()
      .WithHttpTransport()
      .WithToolsFromAssembly();

      // This just disables ASP .Net from console logging.
      builder.Logging.ClearProviders();

      var app = builder.Build();

      // Dont forget this dumbass.
      app.MapMcp();

      return app;
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

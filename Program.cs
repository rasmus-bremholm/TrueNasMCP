using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TrueNasMCP.Settings;
using TrueNasMCP.TrueNas;
using TrueNasMCP.Mcp;

namespace TrueNasMCP;

static class Program
{
    [STAThread]
    static async Task Main(string[] args)
    {
        if (args.Contains("--mcp"))
        {
            await RunMcpServer();
        }
        else
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new TrayApplicationContext());
        }
    }

    private static async Task RunMcpServer()
    {
        var builder = Host.CreateApplicationBuilder();

        var secretsPath = Path.Combine(AppContext.BaseDirectory, "secrets.json");

        builder.Configuration.AddJsonFile(secretsPath, optional: true);

        builder.Services
            .Configure<AppSettings>(builder.Configuration.GetSection("TrueNas"))
            .AddHttpClient<TrueNasClient>()
            .Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithTools<TrueNasTools>();

        builder.Logging.ClearProviders();

        var host = builder.Build();
        await host.RunAsync();
    }
}

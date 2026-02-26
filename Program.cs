using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using TrueNasMCP.Settings;

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

        builder.Configuration.AddJsonFile("secrets.json", optional: true);

        builder.Services
            .Configure<AppSettings>(builder.Configuration.GetSection("TrueNas"))
            .AddHttpClient<TrueNasClient>()
            .Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        builder.Logging.ClearProviders();

        var host = builder.Build();
        await host.RunAsync();
    }
}

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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

        builder.Services
            .AddMcpServer()
            .WithStdioServerTransport()
            .WithToolsFromAssembly();

        builder.Logging.ClearProviders();

        var host = builder.Build();
        await host.RunAsync();
    }
}

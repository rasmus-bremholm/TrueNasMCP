namespace TrueNasMCP.Mcp;

using ModelContextProtocol.Server;
using System.ComponentModel;
using TrueNasMCP.TrueNas;


[McpServerToolType]
public class TrueNasTools
{
   private readonly TrueNasClient _client;

   public TrueNasTools(TrueNasClient client)
   {
      _client = client;
   }

   [McpServerTool, Description("Ping the MCP Server to verify its running.")]
   public static string Ping()
   {
      return "pong - Truenas MCP server is alive!";
   }

   [McpServerTool, Description("Get storage pool information including capacity and health.")]
   public async Task<string> GetStoragePools()
   {
      try
      {
         return await _client.GetRawAsync("pool").ConfigureAwait(false);
      }
      catch (Exception ex)
      {
         return $"ERROR: {ex.GetType().Name}: {ex.Message}";
      }
   }

   [McpServerTool, Description("Get disk information including temperature and SMART health status.")]
public async Task<string> GetDiskHealth()
{
    try
    {
        return await _client.GetRawAsync("disk").ConfigureAwait(false);
    }
    catch (Exception ex)
    {
        return $"ERROR: {ex.GetType().Name}: {ex.Message}";
    }
}

[McpServerTool, Description("Get active system alerts and warnings.")]
public async Task<string> GetAlerts()
{
    try
    {
        return await _client.GetRawAsync("alert/list").ConfigureAwait(false);
    }
    catch (Exception ex)
    {
        return $"ERROR: {ex.GetType().Name}: {ex.Message}";
    }
}

[McpServerTool, Description("Get system information including hostname, uptime and TrueNAS version.")]
public async Task<string> GetSystemInfo()
{
    try
    {
        return await _client.GetRawAsync("system/info").ConfigureAwait(false);
    }
    catch (Exception ex)
    {
        return $"ERROR: {ex.GetType().Name}: {ex.Message}";
    }
}

[McpServerTool, Description("Get dataset information including size and usage per dataset.")]
public async Task<string> GetDatasets()
{
    try
    {
        return await _client.GetRawAsync("pool/dataset").ConfigureAwait(false);
    }
    catch (Exception ex)
    {
        return $"ERROR: {ex.GetType().Name}: {ex.Message}";
    }
}
}

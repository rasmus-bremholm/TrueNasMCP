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
}

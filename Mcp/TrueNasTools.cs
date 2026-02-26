namespace TrueNasMCP.Mcp;

using ModelContextProtocol.Server;
using System.ComponentModel;


[McpServerToolType]
public class TrueNasTools
{
   [McpServerTool, Description("Ping the MCP Server to verify its running.")]
   public static string Ping()
   {
      return "pong - Truenas MCP server is alive!";
   }
}

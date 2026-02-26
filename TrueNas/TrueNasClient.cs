namespace TrueNasMCP.TrueNas;

using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using TrueNasMCP.Settings;

public class TrueNasClient
{
   private readonly HttpClient _http;

   public TrueNasClient(HttpClient http, IOptions<AppSettings> settings)
   {
      _http = http;
      _http.BaseAddress = new Uri(settings.Value.BaseUrl);
      _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.Value.ApiKey);
   }

   public async Task<string> GetRawAsync(string endpoint)
   {
      var response = await _http.GetAsync($"/api/v2.0/{endpoint}");
      response.EnsureSuccessStatusCode();
      return await response.Content.ReadAsStringAsync();
   }
}

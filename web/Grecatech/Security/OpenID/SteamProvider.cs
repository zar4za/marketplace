namespace Grecatech.Security.OpenID
{
    public class SteamProvider
    {
        private const string Endpoint = "https://steamcommunity.com/openid/login?";
        private readonly HttpClient _httpClient;

        public SteamProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> Verify(string query)
        {
            var uri = Endpoint + query.Replace("id_res", "check_authentication");
            var response = await _httpClient.GetStringAsync(uri);
            return response;
        }
    }
}

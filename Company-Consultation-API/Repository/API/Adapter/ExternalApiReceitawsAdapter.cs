using Newtonsoft.Json.Linq;
using Repository.API.Interfaces;

namespace Repository.API.Adapter
{
    public class ExternalApiReceitawsAdapter : IExternalApiReceitawsAdapter
    {
        private readonly HttpClient _httpClient;

        public ExternalApiReceitawsAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<JObject> FetchResultAsync(string cnpj)
        {
            var response = await _httpClient.GetAsync($"https://www.receitaws.com.br/v1/cnpj/{cnpj}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JObject.Parse(content);
        }
    }
}

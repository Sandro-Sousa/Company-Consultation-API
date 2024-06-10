
using Newtonsoft.Json.Linq;

namespace Repository.API.Interfaces
{
    public interface IExternalApiReceitawsAdapter
    {
        Task<JObject> FetchResultAsync(string cnpj);
    }
}

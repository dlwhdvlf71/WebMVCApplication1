using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebMVCApplication1.Models
{
    public class User : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public User(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        //public NamedClientModel(IHttpClientFactory httpClientFactory) => _httpClientFactory = httpClientFactory;

        public int id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }


        public async Task<IEnumerable<User>?> GetUsers()
        {
            var httpClient = _httpClientFactory.CreateClient("FakeAPI");
            var httpResponseMessage = await httpClient.GetAsync("/users");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                return await JsonSerializer.DeserializeAsync<IEnumerable<User>>(contentStream);
            }
            else
            {
                return null;
            }
        }


    }
}

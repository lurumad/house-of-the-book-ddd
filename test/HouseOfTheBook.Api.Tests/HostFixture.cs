using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;

namespace HouseOfTheBook.Api.Tests
{
    public class HostFixture
    {
        private static readonly TestServer server;
        private static readonly HttpClient client;

        static HostFixture()
        {
            var webHostBuilder = new WebHostBuilder()
                .UseEnvironment(EnvironmentName.Development)
                .UseStartup<Startup>();
            server = new TestServer(webHostBuilder);
            client = server.CreateClient();
        }

        public TestServer Server => server;
        public HttpClient Client => client;

        public void Dispose()
        {
            Server.Dispose();
            Client.Dispose();
        }

        public async Task<TEntity> GetAsync<TEntity>(string url)
        {
            var response = await Client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TEntity>(json);
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            var json = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            return await Client.PostAsync(url, json);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await Client.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> PatchAsync(string url)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), url);
            return await Client.SendAsync(request);
        }
    }
}

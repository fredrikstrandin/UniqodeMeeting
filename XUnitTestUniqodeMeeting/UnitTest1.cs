using HeroesWeb.Models;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using XUnitTestUniqodeMeeting.DataAttributes;

namespace XUnitTestUniqodeMeeting
{
    public class UnitTest1
    {
        public class HeroesApi
        {
            private const string DiscoveryEndpoint = "https://server/.well-known/openid-configuration";
            private const string TokenEndpoint = "https://server/connect/token";

            private static TestServer _identityServerServer;
            private static HttpClient _identiyClient { get; }
            public static HttpClient _apiClient { get; }

            private readonly static HttpMessageHandler _handler;

            static HeroesApi()
            {
                var identityServerBuilder = new WebHostBuilder()
                  .UseContentRoot(@"C:\git\UniqodeMeeting\QuickstartIdentityServer")
                  .UseEnvironment("Development")
                  .UseStartup<QuickstartIdentityServer.Startup>()
                  .UseApplicationInsights();

                _identityServerServer = new TestServer(identityServerBuilder);

                _handler = _identityServerServer.CreateHandler();
                _identiyClient = _identityServerServer.CreateClient();

                var apiBuilder = new WebHostBuilder()
                  .UseContentRoot(@"C:\git\UniqodeMeeting\HeroesWeb")
                  .UseEnvironment("Test")
                  .UseStartup<HeroesWeb.Startup>()
                  .ConfigureServices(services =>
                  {
                      services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                      .AddIdentityServerAuthentication(q =>
                            {
                                q.Authority = "https://server";
                                q.JwtBackChannelHandler = _identityServerServer.CreateHandler();
                                q.IntrospectionBackChannelHandler = _identityServerServer.CreateHandler();
                                q.IntrospectionDiscoveryHandler = _identityServerServer.CreateHandler();
                            });
                  })
                  .UseApplicationInsights();


                var apiServer = new TestServer(apiBuilder);

                _apiClient = apiServer.CreateClient();

                var client = new TokenClient(
                    TokenEndpoint,
                    "Test.client",
                    "testing",
                    innerHttpMessageHandler: _handler);

                Task<TokenResponse> task = client.RequestResourceOwnerPasswordAsync("bob", "password", "api1");
                task.Wait();

                TokenResponse response = task.Result;

                _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.AccessToken);

            }

            [Fact]
            public async Task GetHeroes()
            {
                HttpResponseMessage responseApi = await _apiClient.GetAsync("/api/Heroes");

                Assert.Equal(HttpStatusCode.OK, responseApi.StatusCode);
            }

            [Theory]
            [TestHeroes]
            public async Task PostHero(HeroItem item)
            {
                var ApiResponse = await _apiClient.PostAsJsonAsync("/api/Heroes",
                    item);

                ApiResponse.EnsureSuccessStatusCode();

                var responseString = await ApiResponse.Content.ReadAsStringAsync();

                HeroItem respons = JsonConvert.DeserializeObject<HeroItem>(await ApiResponse.Content.ReadAsStringAsync());
                Assert.False(string.IsNullOrEmpty(respons.Id));
            }

            [Theory]
            [TestHeroes]
            public async Task PutHero(HeroItem item)
            {
                item.Name = $"{item.Name} [updatede]";

                var ApiResponse = await _apiClient.PutAsJsonAsync("/api/Heroes",
                    item);

                ApiResponse.EnsureSuccessStatusCode();

                var responseString = await ApiResponse.Content.ReadAsStringAsync();

                HeroItem respons = JsonConvert.DeserializeObject<HeroItem>(await ApiResponse.Content.ReadAsStringAsync());

                Assert.True(respons.Id == item.Id);
                Assert.True(respons.Name == $"{item.Name} [updatede]");
            }

            [Fact]
            public async Task GetGraphQLHeroes()
            {
                var post = new { query = "{  hero(empno: 1) { empNo   city  }}"};

                HttpResponseMessage responseApi = await _apiClient.PostAsJsonAsync("/graphql", post);

                string str = await responseApi.Content.ReadAsStringAsync();
                Assert.Equal(HttpStatusCode.OK, responseApi.StatusCode);
            }

        }
    }
}

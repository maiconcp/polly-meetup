using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TargetServiceConfig.Services
{
    public class ConfigService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;

        public ConfigService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
        }

        public async Task PostOnWithTimeout(int timeout)
        {
            await _httpClient.PostAsync($"api/device/on/{timeout}", new StringContent(string.Empty));
        }


    }

    public interface IConfigService
    {
        Task PostOnWithTimeout(int timeout);
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Services
{
    public class ClientService
    {
        private HttpClient _client = new HttpClient();

        public ClientService()
        {
            _client.BaseAddress = new Uri("http://localhost:5000/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string GetSomeThing()
        {
            try
            {
                return _client.GetStringAsync("api/business").Result;
            }
            catch(AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}

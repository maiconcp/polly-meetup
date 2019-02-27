using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient.Services
{
    public class ClientAlternativeService
    {
        private HttpClient _client = new HttpClient();

        public ClientAlternativeService()
        {
            _client.BaseAddress = new Uri("http://localhost:5000/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public string GetSomeThing()
        {
            try
            {
                return _client.GetStringAsync("api/AlternativeBusiness").Result;
            }
            catch(AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}

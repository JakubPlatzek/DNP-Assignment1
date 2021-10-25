using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Assignment1.Data
{
    public class AdultRemoteData : IAdultsData
    {
        private HttpClientHandler clientHandler;
        private HttpClient httpClient;
        
        public AdultRemoteData()
        {
            clientHandler = new HttpClientHandler();
        }
        
        public async Task<IList<Adult>> GetAdults()
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            String result = await responseMessage.Content.ReadAsStringAsync();

            IList<Adult> adults = JsonSerializer.Deserialize<IList<Adult>>(result, new JsonSerializerOptions(
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return adults;
        }

        public async Task AddAdult(Adult adult)
        {
            using HttpClient client = new HttpClient();

            string todoAsJson = JsonSerializer.Serialize(adult);

            StringContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }

        public async Task RemoveAdult(int id)
        {
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(""); 
            if (!response.IsSuccessStatusCode)
                throw new Exception(@"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");
        }

        public async Task Update(Adult adult)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Adult> Get(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
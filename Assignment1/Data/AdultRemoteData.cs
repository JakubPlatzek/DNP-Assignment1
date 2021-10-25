using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
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
        private HttpClient client;
        
        public AdultRemoteData()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }

        public async Task<IList<Adult>> GetAdults()
        {
            //using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync("https://localhost:5003/Adults");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            String result = await responseMessage.Content.ReadAsStringAsync();

            IList<Adult> adults = JsonSerializer.Deserialize<IList<Adult>>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return adults;
        }

        public async Task AddAdult(Adult adult)
        {
            //using HttpClient client = new HttpClient();

            string todoAsJson = JsonSerializer.Serialize(adult);

            StringContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://localhost:5003/Adults", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task RemoveAdult(int id)
        {
            //using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync($"https://localhost:5003/Adults/{id}");
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task Update(Adult adult)
        {
            //using HttpClient client = new HttpClient();
            string todoAsJson = JsonSerializer.Serialize(adult);
            StringContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PatchAsync("https://localhost:5003/Adults", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }

        public async Task<Adult> Get(int id)
        {
            //using HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:5003/Adults/{id}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            String result = await responseMessage.Content.ReadAsStringAsync();

            Adult adult = JsonSerializer.Deserialize<Adult>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
           return adult;
        }
    }
}
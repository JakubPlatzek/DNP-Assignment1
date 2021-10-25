using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Assignment1.Data
{
    public class UserRemoteData : IUserService
    {
        private HttpClientHandler clientHandler;
        private HttpClient client;

        public UserRemoteData()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            client = new HttpClient(clientHandler);
        }
        public async Task<User> ValidateUser(string userName, string password)
        {
            HttpResponseMessage responseMessage = await client.GetAsync($"https://localhost:5003/Users?userName={userName}&password={password}");
            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Error: {responseMessage.StatusCode}, {responseMessage.ReasonPhrase}");

            String result = await responseMessage.Content.ReadAsStringAsync();
            
            User users = JsonSerializer.Deserialize<User>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return users;
        }

        public async Task AddUser(User user)
        {
            string todoAsJson = JsonSerializer.Serialize(user);

            StringContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync($"https://localhost:5003/Users/{user.Registered}", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
        }
    }
}
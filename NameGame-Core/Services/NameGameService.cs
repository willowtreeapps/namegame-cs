using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WillowTree.NameGame.Core.Models;

namespace WillowTree.NameGame.Core.Services
{
    public interface INameGameService
    {
        Task<List<Person>> GetPeople();
    }

    public class NameGameService : INameGameService
    {
        private static readonly string DataUrl = "http://api.namegame.willowtreemobile.com";

        /// <summary>
        /// Retrieves the people from http://api.namegame.willowtreemobile.com and deserializes them 
        /// into a List<Person>
        /// </summary>        
        public async Task<List<Person>> GetPeople()
        {
            List<Person> people = new List<Person>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(DataUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = Task.Run(() => client.GetAsync(client.BaseAddress)).Result;

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                people = JsonConvert.DeserializeObject<List<Person>>(jsonString);                
            }
            else
            {
                throw new HttpRequestException(string.Format("Namegame API responed with unsuccessful response code: {0}",
                        response.StatusCode));
            }

            return people;
        }
    }
}

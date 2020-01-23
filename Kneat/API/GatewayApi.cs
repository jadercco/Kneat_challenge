using Kneat.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Kneat.API
{
    public class GatewayApi
    {
        private string _urlSwapi = string.Empty;
        private static readonly HttpClient HttpClient = new HttpClient();

        public GatewayApi()
        {

            HttpClient.BaseAddress = new Uri("https://swapi.co/api/");
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        /// <summary>
        /// Returns all ships available in the API
        /// </summary>
        /// <returns>A typed list with the return of the ships available in the API</returns>        
        public IEnumerable<StarshipModel> GetAllStarships()
        {
            List<StarshipModel> result = new List<StarshipModel>();
            BuildResult("starships", result);

            return result;
        }

        /// <summary>
        /// Recursive method responsible for navigation between API pages and deserialization of data.
        /// </summary>
        /// <param name="url">API access url</param>
        /// <param name="starships">List of Ships that will be added to each page found in the API</param>
        private static void BuildResult(string url, List<StarshipModel> starships)
        {

            if (string.IsNullOrEmpty(url)) return;

            HttpResponseMessage message = null;
            try
            {
                message = HttpClient.GetAsync(url).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred. {ex}");
            }

            string result = message.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            StarshipResultModel apiResult = null;

            try
            {
                apiResult = JsonConvert.DeserializeObject<StarshipResultModel>(result);
            }
            catch (JsonReaderException ejs)
            {
                Console.WriteLine($"An error occurred. {ejs}");
            }

            starships.AddRange(apiResult.Results);

            if(apiResult.Next != null)
                BuildResult(apiResult.Next, starships);

        }


    }
}

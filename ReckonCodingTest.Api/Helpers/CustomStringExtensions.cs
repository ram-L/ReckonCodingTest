using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReckonCodingTest.Api.Helpers
{
    public static class CustomStringExtensions
    {
        private const int MAX_API_CALLS = 20;

        public static async Task<T> ApiGetAsync<T>(this string endPoint) where T : class
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            int callCtr = 0;
            do
            {
                response = await client.GetAsync(endPoint);
            } while (!(response?.IsSuccessStatusCode ?? false) && callCtr < MAX_API_CALLS);

            if (callCtr < MAX_API_CALLS)
                return JsonConvert.DeserializeObject<T>(await response?.Content.ReadAsStringAsync());

            return null;
        }

        public static async Task<bool> ApiPostAsync(this string endPoint, string data)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            StringContent content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");

            int callCtr = 0;
            do
            {
                response = await client.PostAsync(endPoint, content);
            } while (!(response?.IsSuccessStatusCode ?? false) && callCtr < MAX_API_CALLS);

            return callCtr < MAX_API_CALLS;
        }

        public static List<int> PositionsOf(this string textToSearch, string subText)
        {
            if (subText == null || subText.Length == 0) return null;
            var positions = new List<int>();

            for (int i = 0; i < textToSearch.Length - subText.Length; i++)
            {
                int j;
                for (j = 0; j < subText.Length; j++)
                    if(textToSearch[i + j].ToString().ToUpper() != subText[j].ToString().ToUpper())
                        break;

                if (j == subText.Length)
                    positions.Add(i + 1);
            }

            return positions.Count > 0 ? positions : null;
        }
    }
}

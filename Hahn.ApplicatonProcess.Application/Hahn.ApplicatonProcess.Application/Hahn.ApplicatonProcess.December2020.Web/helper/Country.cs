using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.helper
{
    public class Country
    {
        private static readonly HttpClient _client = new HttpClient();
        public static async Task<string> GetCountryById(string countryName)
        {
            var ulr = "https://restcountries.eu/rest/v2/name/" + countryName + "" + "?fullText=true";
            var response = await _client.GetAsync(ulr);
            if (response.IsSuccessStatusCode)
            {
                return "OK";
            }
            else
            {
                return "NotFound";
            }
        }
    }
}

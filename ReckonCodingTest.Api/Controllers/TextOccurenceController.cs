using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReckonCodingTest.Api.Helpers;
using ReckonCodingTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ReckonCodingTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextOccurenceController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Get()
        {
            var api = "https://join.reckon.com/test2";

            var textToSearch = (await ($"{api}/textToSearch").ApiGetAsync<TextToSearchResponse>())?.Text;
            var subTexts = (await ($"{api}/subTexts").ApiGetAsync<SubTextsResponse>())?.SubTexts;

            var searchOutput = new SearchOutput
            {
                candidate = "Ramil Grajo",
                text = textToSearch,
                results = new List<SearchResult>()
            };            
            
            if (subTexts != null)
            {
                foreach (var subText in subTexts)
                {
                    string result = null;
                    var subTextPositions = textToSearch.PositionsOf(subText);
                    if (subTextPositions != null)
                        result = string.Join(", ", subTextPositions);
                    searchOutput.results.Add(new SearchResult
                    {
                        subtext = subText,
                        result = string.IsNullOrEmpty(result) ? "<No Output>" : result
                    });
                }
            }

            var jsonOutput = JsonConvert.SerializeObject(searchOutput);
            var postResult = await ($"{api}/submitResults").ApiPostAsync(jsonOutput);

            return jsonOutput;
        }
    }
}

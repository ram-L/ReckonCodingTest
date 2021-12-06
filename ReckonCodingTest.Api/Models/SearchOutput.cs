using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReckonCodingTest.Api.Models
{
    public class SearchOutput
    {
        public string candidate { get; set; }
        public string text { get; set; }
        public List<SearchResult> results { get; set; }
    }

    public class SearchResult
    {
        public string subtext { get; set; }
        public string result { get; set; }
    }
}

using System.Collections.Generic;

namespace series_dotnet.Models.ApiResponse
{
    public class CountryResponse
    {
        public int status { get; set; }
        public string message { get; set; }
        public List<Country> data { get; set; }
    }
}
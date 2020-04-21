using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using series_dotnet.Services.CountryService;

namespace series_dotnet.Controllers
{
    [Route("[controller]")]
    public class CountryController : Controller
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService) 
        {
            _countryService = countryService;
        }

        [HttpGet("GetCountryCodes")]
        public async Task<IActionResult> Get() 
        {
            return Ok(await _countryService.GetAllCountries());
        }
    }   
}
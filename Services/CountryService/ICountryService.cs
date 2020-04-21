using series_dotnet.Models;
using series_dotnet.Models.ApiResponse;
using System.Collections.Generic;
using System.Threading.Tasks;
using series_dotnet.Dtos.Country;

namespace series_dotnet.Services.CountryService
{
    public interface ICountryService
    {
        Task<ServiceResponse<List<Country>>> GetAllCountries();
    }
}
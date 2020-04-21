using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using series_dotnet.Services.CountryService;
using series_dotnet.Models;
using series_dotnet.Dtos.Country;
using System.Net.Http;
using Newtonsoft.Json;
using series_dotnet.Models.ApiResponse;
using System;

namespace series_dotnet.Services.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string baseurl;
        public CountryService(IMapper mapper, IConfiguration configuration) 
        {
            _mapper = mapper;
            _configuration = configuration;
            baseurl = _configuration["AppSettings:APIBaseURL"];
        }

        public async Task<ServiceResponse<List<Country>>> GetAllCountries() 
        {
            ServiceResponse<List<Country>> serviceResponse = new ServiceResponse<List<Country>>();

            var endpoint = "/v1/location/allcountrycodes";
            var url = baseurl + endpoint;
            
            using (var httpClient = new HttpClient()) 
            {
                using( HttpResponseMessage response = await httpClient.GetAsync(url)) 
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();

                    var parsed  = (CountryResponse)JsonConvert.DeserializeObject<CountryResponse>(apiResponse);
                    serviceResponse.Data = parsed.data;
                    return serviceResponse;
                }
            }
        }

    }
}
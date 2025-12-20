using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class LocationServices : ILocationServices
    {
        HttpClient _httpClient;
        public LocationServices(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<List<CityDto>> GetCitiesAsync(int stateId)
        {
            return await _httpClient.GetFromJsonAsync<List<CityDto>>(
                $"https://iran-locations-api.ir/api/v1/fa/cities?state_id={stateId}"
                ) ?? new();
        }

        public async Task<List<StateDto>> GetStatesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<StateDto>>(
                "https://iran-locations-api.ir/api/v1/fa/states"
                ) ?? new();
        }
    }
}

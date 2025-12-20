using MyEshop_Phone.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface ILocationServices
    {
        Task<List<StateDto>> GetStatesAsync();
        Task<List<CityDto>> GetCitiesAsync(int stateId);
    }
}

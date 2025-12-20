using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IColorRepository
    {
        Task<List<_Color>> GetAllAsync();
        Task<_Color> AddAsync(_Color color);
        Task<bool> DeleteAsync(int id);
    }
}

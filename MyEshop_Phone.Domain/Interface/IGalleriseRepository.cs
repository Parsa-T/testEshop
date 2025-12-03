using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IGalleriseRepository
    {
        Task<List<_Products_Galleries>> GetAllAsync(int productId);
        Task<_Products_Galleries?> GetByIdAsync(int id);
        Task AddAsync(_Products_Galleries entity);
        Task DeleteAsync(_Products_Galleries entity);
        Task<_Products_Galleries> ShowGalleris(int id);
        Task SaveChangesAsync();
    }
}

using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IProductsGalleriseServices
    {
        Task<List<AddGalleriseDTO>> GetAllAsync(int productId);
        Task<AddGalleriseDTO?> GetByIdAsync(int id);
        Task AddAsync(AddGalleriseDTO dto);
        Task DeleteAsync(int id);
    }
}

using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class PostalCodeServices : IPostalCodeServices
    {
        ICodePostalRepository _codePostalRepository;
        IUserRepository _userRepository;
        IProductsRepository _productsRepository;
        IOrderRepository _orderRepository;
        public PostalCodeServices(ICodePostalRepository codePostal,IUserRepository user,IProductsRepository products,IOrderRepository orderRepository)
        {
            _codePostalRepository = codePostal;
            _userRepository = user;
            _orderRepository = orderRepository;
            _productsRepository = products;
        }
        public async Task<ShowUserAndProductsDTO> GetAllAsync()
        {
            var listProducts = await _productsRepository.GetAll();
            var listUser = await _userRepository.GetAllUsers();
            var list1 = listProducts.Select(s => new ShowProductDTO
            {
                Id = s.Id,
                Title = s.Title,
            }).ToList();
            var list2 = listUser.Select(s => new ShowUserDTO
            {
                Id = s.Id,
                FullName = s.Name + s.Family,
            }).ToList();
            return new ShowUserAndProductsDTO
            {
                ShowProducts = list1,
                ShowUsers = list2,
            };
        }
    }
}

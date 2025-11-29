using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class ProductsGroupServices : IProductsGroupServices
    {
        IProductsGroupeRepository _productsGroupeRepository;
        public ProductsGroupServices(IProductsGroupeRepository repository)
        {
            _productsGroupeRepository = repository;
        }

        public async Task AddGroups(_Products_Groups products_Groups)
        {
            await _productsGroupeRepository.AddGroupe(products_Groups);
        }

        public async Task<IEnumerable<_Products_Groups>> GetAll()
        {
            return await _productsGroupeRepository.GetAllGroups();
        }

        public async Task<AddOrEditGroupsDTO> GetGroupsForEdit(int id)
        {
            var group = await _productsGroupeRepository.GetGroupsById(id);
            if (group == null)
                return null;
            return new AddOrEditGroupsDTO
            {
                GroupTitle = group.GroupTitle,
                Id = group.Id,
            };
        }

        public async Task RemoveGroups(_Products_Groups groups)
        {
            _productsGroupeRepository.DeleteGroups(groups);
        }

        public async Task Save()
        {
            await _productsGroupeRepository.Save();
        }
    }
}

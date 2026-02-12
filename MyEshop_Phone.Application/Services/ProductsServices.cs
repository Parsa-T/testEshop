using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class ProductsServices : IProductsServices
    {
        IProductsRepository _productsRepository;
        IProductsGroupeRepository _productsGroupeRepository;
        ISubGroupsServices _subGroupsRepository;
        public ProductsServices(IProductsRepository repository, IProductsGroupeRepository productsGroupeRepository,ISubGroupsServices subGroupsServices)
        {
            _productsRepository = repository;
            _productsGroupeRepository = productsGroupeRepository;
            _subGroupsRepository = subGroupsServices;
        }

        public async Task<IEnumerable<_Products>> BestSeller()
        {
            return await _productsRepository.BestSeller();
        }

        public async Task EditProdutcs(ShowProductsDTO dTO)
        {
            var products = await _productsRepository.GetProductsById(dTO.Id);
            if (products == null)
                return;
            products.ShortDescription = dTO.ShortDescription;
            products.Price = dTO.Price;
            products.CreateTime = dTO.CreateTime;
            products.ImageName = dTO.ImageName;
            products.ProductGroupsId = dTO.ProductGroupsId;
            products.Text = dTO.Text;
            products.Title = dTO.Title;
            products.Count = dTO.Count;
            products.SubmenuGroupsId = dTO.SubmenuGroupsId;
            products.RecommendedProducts = dTO.RecommendedProducts;
            await _productsRepository.UpdateProducts(products);

        }

        public async Task<IEnumerable<GetProductsDTO>> GetAll()
        {
            var product = await _productsRepository.GetAllProducts();
            if (product == null)
                return null;
            return product.Select(p => new GetProductsDTO
            {
                CreateTime = p.CreateTime,
                GroupsTitle = p.products_Groups.GroupTitle,
                Price = p.Price,
                ShortDescription = p.ShortDescription,
                Text = p.Text,
                Title = p.Title,
                ImageName = p.ImageName,
                ProductGroupsId = p.ProductGroupsId,
                Id = p.Id
            }).ToList();
        }

        public async Task<IEnumerable<_Products>> GetAllProduct()
        {
            return await _productsRepository.ShowAllProducts();
        }

        public async Task<ShowProductsDTO> GetAllProducts()
        {
            var products = await _productsGroupeRepository.GetAllGroups();
            var subMenu = await _subGroupsRepository.ShowSubMenuGroups();
            var list = products.Select(p => new AddOrEditGroupsDTO
            {
                GroupTitle = p.GroupTitle,
                Id = p.Id,
            }).ToList();
            var list2 = subMenu.Select(sg => new ShowSubMenuDTO
            {
                Id = sg.Id,
                Title = sg.Title,
            }).ToList();
            return new ShowProductsDTO
            {
                ShowGroups = list,
                ShowSubGroups = list2,
            };
        }

        public async Task<int> GetProductsCount()
        {
            return await _productsRepository.ProductsCount();
        }

        public async Task<IEnumerable<ProductsDropdownDTO>> GetProductsDropDown()
        {
            var product = await _productsRepository.GetAll();
            return product.Select(p=>new ProductsDropdownDTO
            {
                Id = p.Id,
                Title = p.Title
            }).ToList();
        }

        public async Task<ShowProductsDTO?> GetProductsForEdit(int id)
        {
            var product = await _productsRepository.GetProductsById(id);
            if(product==null)
                return null;
            var group = await _productsGroupeRepository.GetAllGroups();
            var sub = await _subGroupsRepository.ShowSubMenuGroups();
            return new ShowProductsDTO
            {
                CreateTime = product.CreateTime,
                Id = product.Id,
                ImageName = product.ImageName,
                Price = product.Price,
                ProductGroupsId = product.ProductGroupsId,
                ShortDescription = product.ShortDescription,
                Text = product.Text,
                Title = product.Title,
                Count = product.Count,
                RecommendedProducts = product.RecommendedProducts,
                SubmenuGroupsId = product.SubmenuGroupsId,
                ShowGroups = group.Select(g => new AddOrEditGroupsDTO
                {
                    GroupTitle= g.GroupTitle,
                    Id = g.Id,
                }).ToList(),
                ShowSubGroups = sub.Select(sg=> new ShowSubMenuDTO
                {
                    Id= sg.Id,
                    Title = sg.Title,
                }).ToList()
            };
        }

        public async Task<int> RegisterProducts(_Products dTO)
        {
            //var products = new _Products()
            //{
            //    CreateTime = DateTime.Now,
            //    Id = dTO.Id,
            //    ImageName = dTO.ImageName,
            //    Price = dTO.Price,
            //    ShortDescription = dTO.ShortDescription,
            //    ProductGroupsId = dTO.ProductGroupsId,
            //    Text = dTO.Text,
            //    Title = dTO.Title,
            //};
            await _productsRepository.AddProducts(dTO);
            return dTO.Id;
        }

        public async Task<IEnumerable<_Products>> ReSearch(string search)
        {
            return await _productsRepository.SearchProduct(search);
        }

        public async Task Save()
        {
            await _productsRepository.Save();
        }

        public async Task<IEnumerable<_Products>> ShowGroupsById(int id)
        {
            return await _productsRepository.ShowProductsByGroupsId(id);
        }

        public async Task<IEnumerable<_Products>> ShowProtectionPhone()
        {
            return await _productsRepository.ProtectionPhone();
        }

        public async Task<IEnumerable<_Products>> ShowRecommendedProducts()
        {
            return await _productsRepository.RecommendedProducts();
        }
    }
}

using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class ProductsCommentServices : IProductsCommentServices
    {
        IProductsCommentRepository _productsCommentRepository;
        public ProductsCommentServices(IProductsCommentRepository repository)
        {
            _productsCommentRepository = repository;
        }
        public async Task<int> GetCommentCount()
        {
           return await _productsCommentRepository.CommentCount();
        }
    }
}

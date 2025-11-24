using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Infra.Data.Repository
{
    public class ProductCommentRepository : IProductsCommentRepository
    {
        MyDbContext _db;
        public ProductCommentRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task<int> CommentCount()
        {
            var result = await _db.Products_Comments.CountAsync();
            return result;
        }
    }
}

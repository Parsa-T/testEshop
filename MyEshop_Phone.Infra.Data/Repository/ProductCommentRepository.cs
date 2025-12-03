using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
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

        public async Task DeleteComments(_Products_comment comment)
        {
            _db.Products_Comments.Remove(comment);
        }

        public async Task<_Products_comment> GetCommentById(int id)
        {
            return await _db.Products_Comments.FindAsync(id);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<_Products_comment>> ShowComments()
        {
            return await _db.Products_Comments.Include(c=>c.products).ToListAsync();
        }
    }
}

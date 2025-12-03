using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IProductsCommentRepository
    {
        Task<int> CommentCount();
        Task DeleteComments(_Products_comment comment);
        Task Save();
        Task<_Products_comment> GetCommentById(int id);
        Task<IEnumerable<_Products_comment>> ShowComments();
    }
}

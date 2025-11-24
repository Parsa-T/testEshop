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
    public class UserRepository : IUserRepository
    {
        MyDbContext _db;
        public UserRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task<int> UserCount()
        {
            var result = await _db.Users.CountAsync();
            return result;
        }
    }
}

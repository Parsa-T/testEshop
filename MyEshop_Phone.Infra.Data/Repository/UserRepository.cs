using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
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
    public class UserRepository : IUserRepository
    {
        MyDbContext _db;
        public UserRepository(MyDbContext context)
        {
            _db = context;
        }

        public async Task AddUser(_Users users)
        {
            await _db.Users.AddAsync(users);
        }

        public async Task DeleteUsers(_Users users)
        {
            _db.Users.Remove(users);
        }

        public async Task<bool> FindByNumber(string number)
        {
            try
            {
                return await _db.Users.AnyAsync(u => u.Number == number);

            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public async Task<IEnumerable<_Users>> GetAllUsers()
        {
            var result = await _db.Users.ToListAsync();
            return result;
        }

        public async Task<_Users> GetUserById(int id)
        {
            return await _db.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<_Users> IsExistUser(string number)
        {
            return await _db.Users.SingleOrDefaultAsync(u => u.Number == number);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateUser(_Users users)
        {
            _db.Users.Update(users);
        }

        public async Task<int> UserCount()
        {
            var result = await _db.Users.CountAsync();
            return result;
        }
    }
}

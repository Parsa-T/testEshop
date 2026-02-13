using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<_Users> Users { get; }
        DbSet<_Products> Products { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

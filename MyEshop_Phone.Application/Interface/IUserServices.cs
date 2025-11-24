using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Interface
{
    public interface IUserServices
    {
        Task<int> GetCountUser();
    }
}

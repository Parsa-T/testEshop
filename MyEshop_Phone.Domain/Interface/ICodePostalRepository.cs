using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface ICodePostalRepository
    {
        Task<IEnumerable<_CodePostal>> GetAllPostal();
        Task Save();
        Task Add(_CodePostal codePostal);
        Task<_CodePostal> FindPostalById(int id);
        Task DeletePostal(_CodePostal codePostal);
        Task<List<_CodePostal>> ShowSenderForId(int id);
    }
}

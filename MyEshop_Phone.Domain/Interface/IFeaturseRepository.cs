using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Interface
{
    public interface IFeaturseRepository
    {
        Task AddFeaturse(_Features features);
        Task<_Features?> GetByTitleAsync(string title);
        Task<IEnumerable<_Features>> GetAll();
        Task<_Features> GetByIdAsync(int id);
        Task DeleteFeatures(_Features features);
        Task Save();
    }
}

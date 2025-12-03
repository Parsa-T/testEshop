using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Services
{
    public class FeaturesServices : IFeaturesServices
    {
        IFeaturseRepository _featurseRepository;
        public FeaturesServices(IFeaturseRepository featurseRepository)
        {
            _featurseRepository = featurseRepository;
        }
        public async Task<IEnumerable<FeaturesDropDwonDTO>> GetAllAsync()
        {
            var features = await _featurseRepository.GetAll();
            return features.Select(f => new FeaturesDropDwonDTO
            {
                FeaturesTitle = f.FeaturesTitle,
                Id = f.Id,
            }).ToList();
        }
    }
}

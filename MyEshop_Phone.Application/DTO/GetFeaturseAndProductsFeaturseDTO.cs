using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyEshop_Phone.Application.DTO.GetFeaturseAndProductsFeaturseDTO;

namespace MyEshop_Phone.Application.DTO
{
    public class GetFeaturseAndProductsFeaturseDTO
    {
        public class ProductsFeaturseDTO
        {
            public int Id { get; set; }
            public int FeatureId { get; set; }
            public string FeatureTitle { get; set; }
            public string Value { get; set; }
        }
        public class ProductDetailsDTO
        {
            public int Id { get; set; }
            public string FeaturesTitle { get; set; }

            public List<ProductsFeaturseDTO> Features { get; set; }
        }
        public class AddProductFeatureRequestDTO
        {
            public int ProductId { get; set; }
            public string FeatureTitle { get; set; }
            public string Value { get; set; }
        }
        public class AddProductsFeaturseDTO
        {
            public int Id { get; set; }
            public int ProductsId { get; set; }
            public int FeaturesId { get; set; }
            public string Value { get; set; }
        }
        public class AddOrEditProductsFeaturesDTO
        {
            public int Id { get; set; }
            public int ProductsId { get; set; }
            public int FeaturesId { get; set; }
            public string Value { get; set; }
            public IEnumerable<FeaturesShowDTO> features { get; set; }
        }
        public class FeaturesShowDTO
        {
            public int Id { get; set; }
            [Display(Name = "عنوان ویژگی")]
            [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
            public string FeaturesTitle { get; set; }
        }
    }
}

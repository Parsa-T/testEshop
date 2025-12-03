using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AddFeaturesAndProductsFeaturesDTO
    {
        public int ProductsId { get; set; }
        public IEnumerable<ProductsDropdownDTO> products { get; set; }
        public int FeaturesId { get; set; }
        public IEnumerable<FeaturesDropDwonDTO> features { get; set; }
        [Display(Name ="مقدار")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Value { get; set; }
    }
    public class ProductsDropdownDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class FeaturesDropDwonDTO
    {
        public int Id { get; set; }
        public string FeaturesTitle { get; set; }
    }
}

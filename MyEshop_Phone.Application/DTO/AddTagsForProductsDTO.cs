using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AddTagsForProductsDTO
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public IEnumerable<ProductsDropdownDTO> products { get; set; }
        [Display(Name = "تگ محصول")]
        public string Tag { get; set; }
    }
}

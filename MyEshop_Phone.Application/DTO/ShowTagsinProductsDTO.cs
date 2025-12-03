using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class ShowTagsinProductsDTO
    {
        public int Id { get; set; }
        [Display(Name = "تگ محصول")]
        public string Tag { get; set; }
    }
    public class ShowProductsinTagsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<ShowTagsinProductsDTO> tags { get; set; }
    }
}

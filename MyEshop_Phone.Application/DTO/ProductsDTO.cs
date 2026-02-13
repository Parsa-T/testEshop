using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class ProductsDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string ShortDescription { get; set; }
        public int Price { get; set; }
        public string ImageName { get; set; }
        public int? Count { get; set; }

        public string name => Title;
        public string imageUrl => "/AdminPanel/Photo/Products/" + ImageName;
    }
}

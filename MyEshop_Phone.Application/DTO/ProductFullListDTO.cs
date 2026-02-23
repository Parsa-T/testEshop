using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class ProductFullListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string ImageName { get; set; }
        public DateTime CreateTime { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

        public string GroupName { get; set; }
        public string SubGroupName { get; set; }
    }
}

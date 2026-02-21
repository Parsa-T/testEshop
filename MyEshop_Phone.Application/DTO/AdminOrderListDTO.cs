using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AdminOrderListDTO
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
    }
    public class AdminOrderPdfDto
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public int TotalPrice { get; set; }
        public List<AdminOrderPdfItemDto> Items { get; set; }
    }

    public class AdminOrderPdfItemDto
    {
        public string ProductTitle { get; set; }
        public string ColorName { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.ViewModel
{
    public class ShopCartPageViewModel
    {
        public List<ShowOrderViewModel> Items { get; set; } = new(); // 👈 این خیلی مهمه

        public bool IsLoggedIn { get; set; }

        public string? FullName { get; set; }

        public string? Address { get; set; }

        public string? PostalCode { get; set; }

        public bool HasAddress =>
            !string.IsNullOrWhiteSpace(Address) &&
            !string.IsNullOrWhiteSpace(PostalCode);
    }
}

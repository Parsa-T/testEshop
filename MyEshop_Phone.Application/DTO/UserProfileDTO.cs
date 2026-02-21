using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string? Number { get; set; }
        public string? Address { get; set; }
        public string UrlPhoto { get; set; }
        public int PostalCode { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
    }
    public class EditNumberUsersDTO
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string NewNumber { get; set; }
        public int Code { get; set; }
    }
    public class UserOrderDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public int TotalPrice { get; set; }
        public List<UserOrderDetailDto> Items { get; set; }
    }

    public class UserOrderDetailDto
    {
        public string ProductTitle { get; set; }
        public string ColorName { get; set; }
        public int Count { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
    }
}

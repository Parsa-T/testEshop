using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AddOrEditUsersDTO
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Family { get; set; }
        [Display(Name = "شماره تماس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Number { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Address { get; set; }
        public string UrlPhoto { get; set; }
        [Display(Name = "پروفایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile imgUp { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name ="کدپستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PostalCode { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int? ProductsId { get; set; }
    }
    public class StateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class EditUserProfileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string Address { get; set; }
        public string UrlPhoto { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public IFormFile imgUp { get; set; }
        public int PostalCode { get; set; }
    }
}

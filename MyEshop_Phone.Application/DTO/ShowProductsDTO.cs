using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class ShowProductsDTO
    {
        public int Id { get; set; }
        [Display(Name = "عنوان گروه")]
        public int ProductGroupsId { get; set; }
        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name = "برند")]
        public string ShortDescription { get; set; }
        [Display(Name = "توضیح محصول")]
        public string Text { get; set; }
        [Display(Name = "قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name = "عکس محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }
        [Display(Name = "آپلود عکس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public IFormFile imgUp { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "تعداد محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int? Count { get; set; }
        public IEnumerable<AddOrEditGroupsDTO> ShowGroups { get; set; }
    }
}

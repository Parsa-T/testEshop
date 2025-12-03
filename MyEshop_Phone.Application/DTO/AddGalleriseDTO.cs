using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AddGalleriseDTO
    {
        public int Id { get; set; }

        [Required]
        public int ProductsId { get; set; }

        [Display(Name = "عنوان عکس")]
        [Required]
        public string Title { get; set; }

        // این برای نمایش عکس‌های موجود است، برای اپلود فایل جدید استفاده می‌شود
        public string? ImageName { get; set; }

        // فیلد برای آپلود فایل
        public IFormFile? ImageFile { get; set; }
    }
}

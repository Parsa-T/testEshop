using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Users
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
        public string? Number { get; set; }
        [Display(Name = "آدرس")]
        public string? Address { get; set; }
        [Display(Name = "عکس کاربر")]
        public string UrlPhoto { get; set; }
        [Display(Name = "زمان ثبت نام")]
        public DateTime RegisterDate { get; set; }
        public bool IsAdmin { get; set; }
        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int PostalCode { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; }
        public int? ProductsId { get; set; }

        #region Rel
        public ICollection<_Orders> orders { get; set; }
        [ForeignKey("ProductsId")]
        public virtual _Products products { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Products
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="عنوان گروه")]
        public int ProductGroupsId { get; set; }
        [Display(Name ="نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        [Display(Name ="توضیح مختصر")]
        public string ShortDescription { get; set; }
        [Display(Name ="توضیح محصول")]
        public string Text { get; set; }
        [Display(Name ="قیمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        [Display(Name ="عکس محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string ImageName { get; set; }
        [Display(Name ="تاریخ ایجاد")]
        public DateTime CreateTime { get; set; }
        #region Rel
        public ICollection<_OrderDetails> orderDetails { get; set; }
        public ICollection<_Products_comment> products_Comments { get; set; }
        [ForeignKey("ProductGroupsId")]
        public virtual _Products_Groups products_Groups { get; set; }
        public ICollection<_Products_Features> products_Features { get; set; }
        public ICollection<_Products_Tags> products_Tags { get; set; }
        public ICollection<_Products_Galleries> products_Galleries { get; set; }
        #endregion
    }
}

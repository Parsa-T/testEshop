using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Products_comment
    {
        [Key]
        public int Id { get; set; }
        public int ProductsId { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "متن نظر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200)]
        public string Comment { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTime CreateTime { get; set; }
        public int? ParentId { get; set; }
        #region Rel
        [ForeignKey("ProductsId")]
        public virtual _Products products { get; set; }
        [ForeignKey("ParentId")]
        public virtual _Products_comment Parent { get; set; }

        public virtual ICollection<_Products_comment> Children { get; set; }
            = new List<_Products_comment>();
        #endregion
    }
}

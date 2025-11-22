using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Features
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="عنوان ویژگی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string FeaturesTitle { get; set; }
        #region Rel
        public ICollection<_Products_Features> products_Features { get; set; }
        #endregion
    }
}

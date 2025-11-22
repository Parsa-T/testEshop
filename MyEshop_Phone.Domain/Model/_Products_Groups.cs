using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Products_Groups
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
        #region Rel
        public ICollection<_Products> products { get; set; }
        #endregion
    }
}

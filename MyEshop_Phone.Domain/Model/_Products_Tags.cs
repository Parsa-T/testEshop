using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Products_Tags
    {
        [Key]
        public int Id { get; set; }
        public int ProductsId { get; set; }
        [Display(Name ="تگ محصول")]
        public string Tag { get; set; }
        #region Rel
        [ForeignKey("ProductsId")]
        public virtual _Products products { get; set; }
        #endregion
    }
}

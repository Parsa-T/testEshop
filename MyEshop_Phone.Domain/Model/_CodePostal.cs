using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _CodePostal
    {
        [Key]
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public int UserId { get; set; }
        [Display(Name ="کدپیگیری")]
        public string Code { get; set; }
        #region Rel
        [ForeignKey("ProductsId")]
        public virtual _Products products { get; set; }
        [ForeignKey("UserId")]
        public virtual _Users users { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _ProductsColor
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        #region Rel
        public virtual  _Products products { get; set; }
        public virtual  _Color color { get; set; }
        #endregion
    }
}

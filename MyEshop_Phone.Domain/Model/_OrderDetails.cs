using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }
        #region Rel
        [ForeignKey("OrderId")]
        public virtual _Orders orders { get; set; }
        [ForeignKey("ProductId")]
        public _Products products { get; set; }
        #endregion
    }
}

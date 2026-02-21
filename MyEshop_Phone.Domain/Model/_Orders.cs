using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Orders
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public bool IsFinaly { get; set; }
        public long? RefId { get; set; }
        public string? Authority { get; set; }
        public decimal TotalPrice { get; set; }
        #region Rel
        [ForeignKey("UserId")]
        public virtual _Users users { get; set; }
        public ICollection<_OrderDetails> orderDetails { get; set; }
        #endregion
    }
}

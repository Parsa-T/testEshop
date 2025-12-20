using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        #region Rel
        public ICollection<_ProductsColor> productsColors { get; set; }
        #endregion
    }
}

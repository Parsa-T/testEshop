using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Roles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // جلوگیری از ایدی دهی خودکار 
        public int Id { get; set; }
        [Display(Name ="عنوان کاربر")]
        public string Title { get; set; }
        #region Rel
        public ICollection<_Users> users { get; set; }
        #endregion
    }
}

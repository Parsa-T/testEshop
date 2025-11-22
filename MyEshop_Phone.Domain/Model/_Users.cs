using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _Users
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [Display(Name ="نام")]
        public string Name { get; set; }
        [Display(Name ="نام خانوادگی")]
        public string Family { get; set; }
        [Display(Name ="شماره تماس")]
        public string Number { get; set; }
        [Display(Name ="آدرس")]
        public string Address { get; set; }
        [Display(Name ="عکس کاربر")]
        public string UrlPhoto { get; set; }
        [Display(Name ="زمان ثبت نام")]
        public DateTime RegisterDate { get; set; }

        #region Rel
        [ForeignKey("RoleId")]
        public virtual _Roles roles { get; set; }
        public ICollection<_Orders> orders { get; set; }
        #endregion
    }
}

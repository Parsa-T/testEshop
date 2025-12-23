using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _SubmenuGroups
    {
        [Key]
        public int Id { get; set; }
        public int Products_GroupsId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        #region Rel
        [ForeignKey("Products_GroupsId")]
        public virtual _Products_Groups groups { get; set; }
        #endregion
    }
}

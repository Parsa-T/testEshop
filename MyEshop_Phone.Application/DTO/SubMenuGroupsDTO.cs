using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class SubMenuGroupsDTO
    {
        public int Id { get; set; }
        public int Products_GroupsId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Title { get; set; }
        public IEnumerable<AddOrEditGroupsDTO> ShowAllGroups { get; set; }
    }
}

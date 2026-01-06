using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class AddOrEditGroupsDTO
    {
        public int Id { get; set; }
        [Display(Name = "عنوان محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string GroupTitle { get; set; }
    }
    public class ShowGroupsSubGroupDTO
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; }
        public int SubId { get; set; }
        public string Title { get; set; }
    }
}

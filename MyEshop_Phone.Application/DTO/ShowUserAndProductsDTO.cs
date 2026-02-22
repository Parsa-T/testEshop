using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class ShowUserAndProductsDTO
    {
        public int Id { get; set; }
        public int ProductsId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "کدپیگیری")]
        public string Code { get; set; }
        public IEnumerable<ShowProductDTO> ShowProducts { get; set; }
        public IEnumerable<ShowUserDTO> ShowUsers { get; set; }
    }
    public class ShowProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
    public class ShowUserDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}

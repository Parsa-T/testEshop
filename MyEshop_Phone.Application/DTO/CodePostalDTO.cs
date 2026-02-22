using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.DTO
{
    public class CodePostalDTO
    {
        public string Code { get; set; }
        public List<UserOrderDetailDto> products { get; set; }
        public List<UserProfileDTO> Userss { get; set; }
    }
}

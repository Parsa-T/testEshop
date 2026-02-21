using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Common.model
{
    public class ZarinpalRequestResponse
    {
        public ZarinpalData data { get; set; }
        public object errors { get; set; }
    }

    public class ZarinpalData
    {
        public int code { get; set; }
        public string authority { get; set; }
    }
}

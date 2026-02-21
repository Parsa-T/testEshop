using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Common.model
{
    public class PaymentVerifyResult
    {
        public bool IsSuccess { get; set; }
        public long RefId { get; set; }
    }
    public class ZarinpalVerifyResponse
    {
        public ZarinpalVerifyData data { get; set; }
        public object errors { get; set; }
    }

    public class ZarinpalVerifyData
    {
        public int code { get; set; }
        public long ref_id { get; set; }
    }
}

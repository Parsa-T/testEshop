using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Domain.Model
{
    public class _SmsOtpRequest
    {
        public int Id { get; set; }

        public string Mobile { get; set; }

        public string IpAddress { get; set; }

        public DateTime RequestedAt { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime? BlockedUntil { get; set; }
    }
}

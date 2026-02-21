using MyEshop_Phone.Application.Common.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Application.Common.Interfaces
{
    public interface IPaymentGateway
    {
        Task<string?> RequestPaymentAsync(int orderId, decimal amount);
        Task<PaymentVerifyResult> VerifyAsync(string authority, decimal amount);
    }
}

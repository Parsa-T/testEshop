using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using MyEshop_Phone.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop_Phone.Infra.Data.Repository
{
    public class RequestSmsRepository : ISmsOtpRequestRepository
    {
        MyDbContext _db;
        public RequestSmsRepository(MyDbContext context)
        {
            _db = context;
        }
        public async Task AddRequestSms(_SmsOtpRequest smsOtpRequest)
        {
            await _db.smsOtpRequests.AddAsync(smsOtpRequest);
        }

        public async Task Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}

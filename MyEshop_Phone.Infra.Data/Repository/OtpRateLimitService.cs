using Microsoft.EntityFrameworkCore;
using MyEshop_Phone.Application.Common.Interfaces;
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
    public class OtpRateLimitService : IOtpRateLimitService
    {
        private readonly MyDbContext _context;

        public OtpRateLimitService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CanSendOtpAsync(string mobile, string ipAddress, bool isRegister)
        {
            var now = DateTime.UtcNow;

            int limit = isRegister ? 10 : 5;
            int blockHours = isRegister ? 3 : 1;

            var lastRequest = await _context.smsOtpRequests
                .Where(x => x.Mobile == mobile)
                .OrderByDescending(x => x.RequestedAt)
                .FirstOrDefaultAsync();

            // ⭐ اگر کاربر بلاک است
            if (lastRequest != null &&
                lastRequest.IsBlocked &&
                lastRequest.BlockedUntil > now)
            {
                return false;
            }

            // ⭐ اگر بلاک منقضی شده → آزاد کن
            if (lastRequest != null &&
                lastRequest.BlockedUntil != null &&
                lastRequest.BlockedUntil <= now)
            {
                lastRequest.IsBlocked = false;
                lastRequest.BlockedUntil = null;
            }

            // ⭐ شمارش درخواست‌ها
            var requestCount = await _context.smsOtpRequests
                .CountAsync(x =>
                    x.Mobile == mobile &&
                    x.RequestedAt >= now.AddHours(-3));

            if (requestCount >= limit)
            {
                if (lastRequest != null)
                {
                    lastRequest.IsBlocked = true;
                    lastRequest.BlockedUntil = now.AddHours(blockHours);
                }
                else
                {
                    _context.smsOtpRequests.Add(new _SmsOtpRequest
                    {
                        Mobile = mobile,
                        IpAddress = ipAddress,
                        RequestedAt = now,
                        IsBlocked = true,
                        BlockedUntil = now.AddHours(blockHours)
                    });
                }

                await _context.SaveChangesAsync();
                return false;
            }

            return true;
        }
    }
}

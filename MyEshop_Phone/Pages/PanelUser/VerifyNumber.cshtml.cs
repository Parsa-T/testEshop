using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;

namespace MyEshop_Phone.Pages.PanelUser
{
    public class VerifyNumberModel : PageModel
    {
        IUserServices _userServices;
        SendSmsSercives _sendSmsSercives;
        public VerifyNumberModel(IUserServices user, SendSmsSercives sendSms)
        {
            _sendSmsSercives = sendSms;
            _userServices = user;
        }
        [BindProperty]
        public EditNumberUsersDTO EditNumber { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var enteredCode = EditNumber.Code;
            var sessionCode = HttpContext.Session.GetString("VerifyCode");
            var newNumber = HttpContext.Session.GetString("NewNumber");
            var userId = HttpContext.Session.GetInt32("UserId");

            if (enteredCode == int.Parse(sessionCode))
            {
                await _userServices.UpdateUserNumber(userId.Value, newNumber);
                return RedirectToPage("index");
            }

            ModelState.AddModelError("", "کد وارد شده اشتباه است");
            return Page();
        }
        public async Task<JsonResult> OnPostResendCode()
        {
            var newNumber = HttpContext.Session.GetString("NewNumber");

            if (string.IsNullOrEmpty(newNumber))
                return new JsonResult(new { success = false });

            var lastSendTicks = HttpContext.Session.GetString("LastSendSmS");

            if (!string.IsNullOrEmpty(lastSendTicks))
            {
                var lastSendTime = DateTime.Parse(lastSendTicks);

                if (DateTime.Now - lastSendTime < TimeSpan.FromSeconds(120))
                {
                    return new JsonResult(new { success = false });
                }
            }

            // تولید کد
            Random rd = new Random();
            var code = rd.Next(1000, 9999).ToString();

            await _sendSmsSercives.SendOtpAsync(newNumber, code);

            // ذخیره کد جدید
            HttpContext.Session.SetString("VerifyCode", code);

            // ذخیره زمان ارسال جدید
            HttpContext.Session.SetString("LastSendSmS", DateTime.Now.ToString());

            return new JsonResult(new { success = true });
        }


    }
}

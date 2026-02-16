using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;

namespace MyEshop_Phone.Pages.PanelUser
{
    public class EditNumberUsersModel : PageModel
    {
        IUserServices _userServices;
        SendSmsSercives _sendSmsSercives;
        public EditNumberUsersModel(IUserServices user, SendSmsSercives sendSmsSercives)
        {
            _userServices = user;
            _sendSmsSercives = sendSmsSercives;
        }
        [BindProperty]
        public EditNumberUsersDTO EditNumber { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            EditNumber = await _userServices.FindUserIdForEditNumber(id);
            if (EditNumber == null)
                return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            ModelState.Remove("EditNumber.Code");
            if(!ModelState.IsValid)
                return Page();
            if (string.IsNullOrEmpty(EditNumber.NewNumber))
            {
                ModelState.AddModelError("", "لطفا شماره تماس جدید رو پر کنید");
                return Page();
            }
            string code = GenerateCode();
            //var result = await _sendSmsSercives.SendOtpAsync(EditNumber.NewNumber, code);
            HttpContext.Session.SetString("LastSendSmS", DateTime.Now.ToString());
            HttpContext.Session.SetString("VerifyCode", code);
            HttpContext.Session.SetString("NewNumber", EditNumber.NewNumber);
            HttpContext.Session.SetInt32("UserId", EditNumber.Id);
            return RedirectToPage("VerifyNumber");
        }
        private string GenerateCode()
        {
            Random rnd = new Random();
            return rnd.Next(1000,9999).ToString();
        }
    }
}

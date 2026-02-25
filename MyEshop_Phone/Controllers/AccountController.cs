using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;
using MyEshop_Phone.Application.Utilitise;
using MyEshop_Phone.Application.ViewModel;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using MyEshop_Phone.Pages.Admin.Color;
using System.Security.Claims;

namespace MyEshop_Phone.Controllers
{
    public class AccountController : Controller
    {
        IUserServices _userServices;
        IUserRepository _userRepository;
        SendSmsSercives _sendSmsSercives;
        public AccountController(IUserServices services, IUserRepository repository, SendSmsSercives send)
        {
            _userServices = services;
            _userRepository = repository;
            _sendSmsSercives = send;
        }

        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterUserDTO dTO)
        {
            if (string.IsNullOrEmpty(dTO.Number))
            {
                ModelState.AddModelError("Number", "شماره وارد شده صحیح نمیباشد");
                return View();
            }
            if (await _userServices.FindNumberAsync(dTO.Number))
            {
                return View("Login");
            }
            var code = new Random().Next(1000, 9999).ToString();
            var result = await _sendSmsSercives.SendOtpAsync(dTO.Number, code);
            if (!result)
            {
                ViewBag.Error = "خطا در ارسال پیامک";
                return View();
            }
            //ذخیره موقت دیتا
            HttpContext.Session.SetString("OtpCode", code);
            HttpContext.Session.SetString("RegisterName", dTO.Name);
            HttpContext.Session.SetString("RegisterFamily", dTO.Family);
            HttpContext.Session.SetString("RegisterNumber", dTO.Number);
            return RedirectToAction("VerifyCode");
        }
        [HttpGet]
        [Route("VerifyCode")]
        public IActionResult VerifyCode()
        {
            return View();
        }
        [HttpPost]
        [Route("VerifyCode")]
        public async Task<IActionResult> VerifyCode(VerifyCodeViewModel userCode)
        {
            var savedCode = HttpContext.Session.GetString("OtpCode");

            if (savedCode == null)
                return RedirectToAction("Register");

            if (userCode.Code != savedCode)
            {
                ViewBag.Error = "کد وارد شده اشتباه است";
                return View();
            }
            _Users user = new _Users()
            {
                Name = HttpContext.Session.GetString("RegisterName"),
                Family = HttpContext.Session.GetString("RegisterFamily"),
                Number = HttpContext.Session.GetString("RegisterNumber"),
                IsAdmin = false,
                RegisterDate = DateTime.Now,
            };
            await _userServices.AddUsers(user);
            await _userServices.SaveAsync();
            //پاک کردن دیتا موقت
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        #endregion
        //edit
        #region Login
        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUsersDTO dTO, string ReturnUrl = "/")
        {
            if (!ModelState.IsValid)
                return View();
            var user = await _userRepository.IsExistUser(dTO.Number);
            if (user == null)
            {
                ViewBag.User = "شماره ای یافت نشد";
                return RedirectToAction("Register");
            }
            var code = new Random().Next(1000, 9999).ToString();
            var result = await _sendSmsSercives.SendOtpAsync(dTO.Number, code);
            HttpContext.Session.SetString("OtpCode", code);
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("RememberMe", dTO.RememberMe.ToString());
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
            return RedirectToAction("VerifyLoginCode");
        }
        public async Task<IActionResult> VerifyLoginCode()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyLoginCode(VerifyCodeViewModel userCode)
        {
            if (userCode == null || string.IsNullOrEmpty(userCode.Code))
                return View();

            var saveCode = HttpContext.Session.GetString("OtpCode");
            if (string.IsNullOrEmpty(saveCode))
                return RedirectToAction("Login");

            if (saveCode != userCode.Code)
                return RedirectToAction("Login");

            var userId = HttpContext.Session.GetString("UserId");
            var name = HttpContext.Session.GetString("UserName");
            var admin = HttpContext.Session.GetString("IsAdmin");
            var rememberMeString = HttpContext.Session.GetString("RememberMe");

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(name))
                return RedirectToAction("Login");

            bool isAdmin = false;
            bool rememberMe = false;

            bool.TryParse(admin, out isAdmin);
            bool.TryParse(rememberMeString, out rememberMe);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Name, name),
        new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
    };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                AllowRefresh = true,
                ExpiresUtc = rememberMe
                    ? DateTimeOffset.UtcNow.AddDays(30)   // اگر RememberMe تیک خورده
                    : DateTimeOffset.UtcNow.AddHours(2)  // اگر تیک نخورده
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                properties);

            // پاک کردن اطلاعات Session مربوط به ورود
            HttpContext.Session.Remove("OtpCode");
            HttpContext.Session.Remove("RememberMe");

            return Redirect("/");
        }
        #endregion


        [Route("Loguot")]
        public async Task<IActionResult> Loguot()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Redirect("/");
        }
    }
}

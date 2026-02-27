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
using MyEshop_Phone.Infra.Data.Repository;

namespace MyEshop_Phone.Controllers
{
    public class AccountController : Controller
    {
        IUserServices _userServices;
        IUserRepository _userRepository;
        SendSmsSercives _sendSmsSercives;
        IOtpRateLimitService _otpRateLimitService;
        public AccountController(IUserServices services, IUserRepository repository, SendSmsSercives send, IOtpRateLimitService otpRateLimitService)
        {
            _userServices = services;
            _userRepository = repository;
            _sendSmsSercives = send;
            _otpRateLimitService = otpRateLimitService;
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
            if (!ModelState.IsValid)
                return View();

            if (string.IsNullOrEmpty(dTO.Number))
            {
                ModelState.AddModelError("Number", "شماره وارد شده صحیح نمیباشد");
                return View();
            }

            // جلوگیری از ثبت نام تکراری
            if (await _userServices.FindNumberAsync(dTO.Number))
            {
                return RedirectToAction("Login");
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var canSendOtp = await _otpRateLimitService
                .CanSendOtpAsync(dTO.Number, ipAddress, true);

            if (!canSendOtp)
            {
                ViewBag.Error = "تعداد درخواست بیش از حد مجاز است";
                return View();
            }

            var code = Random.Shared.Next(100000, 999999).ToString();

            var result = await _sendSmsSercives.SendOtpAsync(dTO.Number, code);

            if (!result)
            {
                ViewBag.Error = "خطا در ارسال پیامک";
                return View();
            }

            HttpContext.Session.SetString("OtpCode", code);
            HttpContext.Session.SetString("OtpExpire",
                DateTime.UtcNow.AddMinutes(2).ToString());

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
            if (userCode == null || string.IsNullOrEmpty(userCode.Code))
                return View();

            var savedCode = HttpContext.Session.GetString("OtpCode");
            var expire = HttpContext.Session.GetString("OtpExpire");

            if (string.IsNullOrEmpty(savedCode) || string.IsNullOrEmpty(expire))
                return RedirectToAction("Register");

            if (!DateTime.TryParse(expire, out var expireDate) ||
                expireDate < DateTime.UtcNow)
            {
                ViewBag.Error = "کد منقضی شده است";
                return View();
            }

            if (savedCode != userCode.Code)
            {
                ViewBag.Error = "کد وارد شده اشتباه است";
                return View();
            }

            var user = new _Users()
            {
                Name = HttpContext.Session.GetString("RegisterName"),
                Family = HttpContext.Session.GetString("RegisterFamily"),
                Number = HttpContext.Session.GetString("RegisterNumber"),
                IsAdmin = false,
                RegisterDate = DateTime.Now
            };

            await _userServices.AddUsers(user);
            await _userServices.SaveAsync();

            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
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
                return RedirectToAction("Register");
            }

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            var canSendOtp = await _otpRateLimitService
                .CanSendOtpAsync(dTO.Number, ipAddress, false);

            if (!canSendOtp)
            {
                ViewBag.Error = "تعداد درخواست بیش از حد مجاز است";
                return View();
            }

            var code = Random.Shared.Next(100000, 999999).ToString();

            var result = await _sendSmsSercives.SendOtpAsync(dTO.Number, code);

            if (!result)
            {
                ViewBag.Error = "خطا در ارسال پیامک";
                return View();
            }

            HttpContext.Session.SetString("OtpCode", code);
            HttpContext.Session.SetString("OtpExpire",
                DateTime.UtcNow.AddMinutes(2).ToString());

            HttpContext.Session.SetString("UserId", user.Id.ToString());
            HttpContext.Session.SetString("UserName", user.Name);
            HttpContext.Session.SetString("RememberMe", dTO.RememberMe.ToString());
            HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());

            return RedirectToAction("VerifyLoginCode", new { ReturnUrl = ReturnUrl });
        }
        [HttpGet]
        public IActionResult VerifyLoginCode(string ReturnUrl = "/")
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VerifyLoginCode(VerifyCodeViewModel userCode, string ReturnUrl = "/")
        {
            var savedCode = HttpContext.Session.GetString("OtpCode");
            var expire = HttpContext.Session.GetString("OtpExpire");

            if (string.IsNullOrEmpty(savedCode) || string.IsNullOrEmpty(expire))
                return RedirectToAction("Login");

            if (!DateTime.TryParse(expire, out var expireDate) ||
                expireDate < DateTime.UtcNow)
            {
                ViewBag.Error = "کد منقضی شده است";
                return View();
            }

            if (savedCode != userCode.Code)
            {
                ViewBag.Error = "کد وارد شده اشتباه است";
                return View();
            }

            var userId = HttpContext.Session.GetString("UserId");
            var name = HttpContext.Session.GetString("UserName");
            var admin = HttpContext.Session.GetString("IsAdmin");
            var rememberMeString = HttpContext.Session.GetString("RememberMe");

            bool.TryParse(admin, out var isAdmin);
            bool.TryParse(rememberMeString, out var rememberMe);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, userId),
        new Claim(ClaimTypes.Name, name),
        new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
    };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe
                    ? DateTimeOffset.UtcNow.AddDays(30)
                    : DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                properties);

            HttpContext.Session.Clear();

            if (!Url.IsLocalUrl(ReturnUrl))
                ReturnUrl = "/";

            return LocalRedirect(ReturnUrl);
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

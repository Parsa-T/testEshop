using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyEshop_Phone.Application.DTO;
using MyEshop_Phone.Application.Interface;
using MyEshop_Phone.Application.Services;
using MyEshop_Phone.Application.ViewModel;
using MyEshop_Phone.Domain.Interface;
using MyEshop_Phone.Domain.Model;
using System.Security.Claims;

namespace MyEshop_Phone.Controllers
{
    public class AccountController : Controller
    {
        private const int LoginOtpCooldownSeconds = 120;
        private const string SessionLoginOtpCode = "LoginOtpCode";
        private const string SessionLoginOtpPhone = "LoginOtpPhone";
        private const string SessionLoginOtpUserId = "LoginOtpUserId";
        private const string SessionLoginOtpUserName = "LoginOtpUserName";
        private const string SessionLoginOtpIsAdmin = "LoginOtpIsAdmin";
        private const string SessionLoginOtpRememberMe = "LoginOtpRememberMe";
        private const string SessionLoginOtpLastSentUnix = "LoginOtpLastSentUnix";

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
                ClearLoginOtpSession();
                ViewBag.User = "شماره ای یافت نشد";
                return RedirectToAction("Register");
            }

            ClearLoginOtpSession();
            var code = GenerateOtpCode();
            var result = await _sendSmsSercives.SendOtpAsync(dTO.Number, code);
            if (!result)
            {
                ViewBag.Error = "ارسال پیامک با خطا مواجه شد، لطفاً دوباره تلاش کنید";
                return View(dTO);
            }

            SetLoginOtpSession(user, dTO.Number, dTO.RememberMe, code);
            return RedirectToAction("VerifyLoginCode");
        }
        public IActionResult VerifyLoginCode()
        {
            if (!HasPendingLoginOtpSession())
                return RedirectToAction("Login");

            ViewBag.RemainingSeconds = CalculateRemainingSeconds(GetSessionLong(SessionLoginOtpLastSentUnix));
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyLoginCode(VerifyCodeViewModel userCode)
        {
            if (userCode == null || string.IsNullOrEmpty(userCode.Code))
            {
                ViewBag.RemainingSeconds = CalculateRemainingSeconds(GetSessionLong(SessionLoginOtpLastSentUnix));
                return View();
            }

            var saveCode = HttpContext.Session.GetString(SessionLoginOtpCode);
            if (string.IsNullOrEmpty(saveCode))
                return RedirectToAction("Login");

            if (saveCode != userCode.Code)
                return RedirectToAction("Login");

            var userId = HttpContext.Session.GetString(SessionLoginOtpUserId);
            var name = HttpContext.Session.GetString(SessionLoginOtpUserName);
            var admin = HttpContext.Session.GetString(SessionLoginOtpIsAdmin);
            var rememberMeString = HttpContext.Session.GetString(SessionLoginOtpRememberMe);

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

            ClearLoginOtpSession();

            return Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendLoginCode()
        {
            if (!HasPendingLoginOtpSession())
            {
                return Json(new
                {
                    success = false,
                    redirectToLogin = true,
                    message = "نشست منقضی شده است"
                });
            }

            var remainingSeconds = CalculateRemainingSeconds(GetSessionLong(SessionLoginOtpLastSentUnix));
            if (remainingSeconds > 0)
            {
                return Json(new
                {
                    success = false,
                    remainingSeconds,
                    message = "تا پایان زمان باقی‌مانده صبر کنید"
                });
            }

            var phone = HttpContext.Session.GetString(SessionLoginOtpPhone);
            if (string.IsNullOrWhiteSpace(phone))
            {
                return Json(new
                {
                    success = false,
                    redirectToLogin = true,
                    message = "نشست منقضی شده است"
                });
            }

            var newCode = GenerateOtpCode();
            var sendResult = await _sendSmsSercives.SendOtpAsync(phone, newCode);
            if (!sendResult)
            {
                return Json(new
                {
                    success = false,
                    remainingSeconds = 0,
                    message = "ارسال پیامک ناموفق بود"
                });
            }

            HttpContext.Session.SetString(SessionLoginOtpCode, newCode);
            HttpContext.Session.SetString(SessionLoginOtpLastSentUnix, GetCurrentUnixTimeSeconds().ToString());

            return Json(new
            {
                success = true,
                remainingSeconds = LoginOtpCooldownSeconds,
                message = "کد جدید ارسال شد"
            });
        }
        #endregion


        [Route("Loguot")]
        public async Task<IActionResult> Loguot()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Redirect("/");
        }

        private static string GenerateOtpCode()
        {
            return Random.Shared.Next(1000, 9999).ToString();
        }

        private static long GetCurrentUnixTimeSeconds()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        }

        private long? GetSessionLong(string key)
        {
            var value = HttpContext.Session.GetString(key);
            if (!long.TryParse(value, out var parsed))
                return null;

            return parsed;
        }

        private static int CalculateRemainingSeconds(long? lastSentUnix)
        {
            if (!lastSentUnix.HasValue)
                return 0;

            var elapsed = GetCurrentUnixTimeSeconds() - lastSentUnix.Value;
            if (elapsed < 0)
                elapsed = 0;

            var remaining = LoginOtpCooldownSeconds - (int)elapsed;
            return remaining > 0 ? remaining : 0;
        }

        private bool HasPendingLoginOtpSession()
        {
            return !string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionLoginOtpCode))
                && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionLoginOtpPhone))
                && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionLoginOtpUserId))
                && !string.IsNullOrWhiteSpace(HttpContext.Session.GetString(SessionLoginOtpUserName));
        }

        private void SetLoginOtpSession(_Users user, string phone, bool rememberMe, string code)
        {
            ClearLoginOtpSession();
            HttpContext.Session.SetString(SessionLoginOtpCode, code);
            HttpContext.Session.SetString(SessionLoginOtpPhone, phone);
            HttpContext.Session.SetString(SessionLoginOtpUserId, user.Id.ToString());
            HttpContext.Session.SetString(SessionLoginOtpUserName, user.Name);
            HttpContext.Session.SetString(SessionLoginOtpRememberMe, rememberMe.ToString());
            HttpContext.Session.SetString(SessionLoginOtpIsAdmin, user.IsAdmin.ToString());
            HttpContext.Session.SetString(SessionLoginOtpLastSentUnix, GetCurrentUnixTimeSeconds().ToString());
        }

        private void ClearLoginOtpSession()
        {
            HttpContext.Session.Remove(SessionLoginOtpCode);
            HttpContext.Session.Remove(SessionLoginOtpPhone);
            HttpContext.Session.Remove(SessionLoginOtpUserId);
            HttpContext.Session.Remove(SessionLoginOtpUserName);
            HttpContext.Session.Remove(SessionLoginOtpRememberMe);
            HttpContext.Session.Remove(SessionLoginOtpIsAdmin);
            HttpContext.Session.Remove(SessionLoginOtpLastSentUnix);
        }
    }
}

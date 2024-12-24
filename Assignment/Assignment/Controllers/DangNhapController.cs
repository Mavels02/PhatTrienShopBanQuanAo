using Assignment.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Assignment.Controllers
{
    public class DangNhapController : Controller
    {
        readonly AsmC4Context _context;
        public DangNhapController(AsmC4Context context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(us => us.Email == email && us.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.HoTen),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.ChucVu ?? "KH"),
                    new Claim("IdUser", user.IdUser.ToString()) // Thêm IdUser vào claims
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Thông tin đăng nhập không hợp lệ.";
            return View();
        }
    }
}

using chineseArtwork.Models;
using chineseArtwork.Services.Interfaces;
using chineseArtwork.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace chineseArtwork.Controllers.Admin
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly ChineseArtworkContext _context;
        private readonly IPasswordService _passwordService;

        public AdminController(ChineseArtworkContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl = null)
        {
            // 如果用戶已登入，重定向到後台首頁
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());  // 傳遞一個空模型到視圖
        }

        [HttpPost("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);  // 模型驗證失敗，返回視圖並顯示錯誤
            }

            // 從數據庫查詢藝術家
            var artist = await _context.Artists
                .FirstOrDefaultAsync(a => a.ArtAccount == model.Account && a.ArtStatus == 1);

            // 驗證密碼
            if (artist == null || !_passwordService.VerifyPassword(artist.ArtPassword, model.Password))
            {
                ModelState.AddModelError(string.Empty, "帳號或密碼錯誤");
                return View(model);
            }

            // 創建身份聲明
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, artist.ArtName),
                new Claim(ClaimTypes.NameIdentifier, artist.ArtId.ToString()),
                new Claim(ClaimTypes.Email, artist.ArtEmail),
                new Claim("AccountType", "Artist"),
                new Claim("ArtistId", artist.ArtId.ToString())
            };

            // 添加權限聲明（如果有授權資料）
            var authorities = await _context.Authorities
                .Include(a => a.Func)
                .Where(a => a.ArtId == artist.ArtId && a.AuthStatus == 1)
                .ToListAsync();

            foreach (var authority in authorities)
            {
                claims.Add(new Claim(ClaimTypes.Role, authority.Func.FuncName));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe,
                ExpiresUtc = model.RememberMe ? DateTimeOffset.UtcNow.AddDays(30) : DateTimeOffset.UtcNow.AddDays(1)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // 登入成功，重定向到指定頁面或默認頁面
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
        }


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // 添加一個重設密碼方法（可選）
        [HttpGet("reset-password")]
        public IActionResult ResetPassword()
        {
            // 重置密碼視圖
            return View();
        }

        [Authorize]
        [HttpGet("change-password")]
        public IActionResult ChangePassword()
        {
            // 更改密碼視圖
            return View();
        }
    }
}

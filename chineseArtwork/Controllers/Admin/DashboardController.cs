using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chineseArtwork.Controllers.Admin
{
    [Area("Admin")]
    [Authorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            // 用戶的身份信息可以從 User 對象獲取
            ViewData["UserName"] = User?.Identity?.Name ?? "User";
            return View();
        }
    }
}

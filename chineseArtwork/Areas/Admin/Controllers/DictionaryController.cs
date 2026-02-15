using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace chineseArtwork.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class DictionaryController : Controller
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

using Microsoft.AspNetCore.Mvc;
namespace chineseArtwork.Controllers.Admin
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
    }
}

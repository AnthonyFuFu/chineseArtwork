using Microsoft.AspNetCore.Mvc;

namespace chineseArtwork.Areas.Admin.Components
{
    public class SideMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

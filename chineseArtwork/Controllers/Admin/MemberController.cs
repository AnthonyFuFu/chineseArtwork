using chineseArtwork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace chineseArtwork.Controllers.Admin
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }

        public IActionResult Details(int id)
        {
            var member = _memberService.GetMemberById(id);
            return View(member);
        }
    }
}

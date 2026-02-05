using chineseArtwork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace chineseArtwork.Controllers.Admin
{
    [Route("admin/[controller]")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        [HttpGet("list")]
        public IActionResult List()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var member = _memberService.GetMemberById(id);
            return View(member);
        }
    }
}

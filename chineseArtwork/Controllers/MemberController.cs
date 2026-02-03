using chineseArtwork.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace chineseArtwork.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpGet]
    public IActionResult GetMembers()
    {
        var members = _memberService.GetAllMembers();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public IActionResult GetMember(int id)
    {
        var member = _memberService.GetMemberById(id);
        if (member == null) return NotFound();
        return Ok(member);
    }
}
using chineseArtwork.Models;

namespace chineseArtwork.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAllMembers();
        Member GetMemberById(int id);
    }
}

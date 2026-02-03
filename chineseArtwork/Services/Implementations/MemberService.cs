using chineseArtwork.Dao.Interfaces;
using chineseArtwork.Models;
using chineseArtwork.Services.Interfaces;

namespace chineseArtwork.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly IMemberDao _memberDao;

        public MemberService(IMemberDao memberDao)
        {
            _memberDao = memberDao;
        }

        public IEnumerable<Member> GetAllMembers()
        {
            return _memberDao.GetAll();
        }

        public Member GetMemberById(int id)
        {
            return _memberDao.GetById(id);
        }
    }
}

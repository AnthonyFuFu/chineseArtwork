using chineseArtwork.Dao.Interfaces;
using chineseArtwork.Models;

namespace chineseArtwork.Dao.Implementations
{
    public class MemberDao : IMemberDao
    {
        private readonly ChineseArtworkContext _context;

        public MemberDao(ChineseArtworkContext context)
        {
            _context = context;
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public Member GetById(int id)
        {
            return _context.Members.FirstOrDefault(m => m.MemId == id);
        }
    }
}

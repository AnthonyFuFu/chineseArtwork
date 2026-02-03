using chineseArtwork.Models;

namespace chineseArtwork.Dao.Interfaces
{
    public interface IMemberDao
    {
        IEnumerable<Member> GetAll();
        Member GetById(int id);
    }
}

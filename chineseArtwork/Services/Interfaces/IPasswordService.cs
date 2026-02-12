namespace chineseArtwork.Services.Interfaces
{
    public interface IPasswordService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string storedHash, string password);
    }
}

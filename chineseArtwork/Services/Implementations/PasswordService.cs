using chineseArtwork.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace chineseArtwork.Services.Implementations
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            // 生成一個隨機的 salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 使用 PBKDF2 演算法生成哈希
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // 將 salt 和哈希組合在一起，以便以後驗證
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        public bool VerifyPassword(string storedHash, string password)
        {
            // 如果沒有存儲的哈希值，則無法驗證
            if (string.IsNullOrEmpty(storedHash))
                return false;

            // 檢查存儲的哈希是否包含 salt
            var parts = storedHash.Split(':');
            if (parts.Length == 2)
            {
                // 新格式：salt:hash
                var salt = Convert.FromBase64String(parts[0]);
                var hash = parts[1];

                // 使用相同的演算法和 salt 重新計算哈希
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));

                // 比較計算出的哈希與存儲的哈希
                return hash == hashed;
            }
            else
            {
                // 舊格式：明文或其他格式（假設是明文密碼）
                // 註意：這部分是為了與現有數據兼容，在生產環境中應該逐步淘汰
                return storedHash == password;
            }
        }
    }
}

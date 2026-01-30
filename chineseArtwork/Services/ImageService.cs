using Microsoft.AspNetCore.Hosting;
using chineseArtwork.Models;
using chineseArtwork.Utils;

namespace chineseArtwork.Services
{
    public interface IImageService
    {
        Task<ImageResponseModel> SaveImageAsync(ImageUploadRequest request);
    }
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<ImageResponseModel> SaveImageAsync(ImageUploadRequest request)
        {
            var tableName = request.TableName; // 例如: artwork、user
            var uploadFolder = Path.Combine(_environment.WebRootPath, "upload", "images", tableName);
            var cacheFolder = Path.Combine(_environment.WebRootPath, "upload", "images", "cache", tableName);

            // 確保目錄存在
            Directory.CreateDirectory(uploadFolder);
            Directory.CreateDirectory(cacheFolder);

            // 原圖路徑
            var originalFilePath = Path.Combine(uploadFolder, request.File.FileName);

            // 保存原圖
            using (var stream = new FileStream(originalFilePath, FileMode.Create))
            {
                await request.File.CopyToAsync(stream);
            }

            // 壓縮圖路徑
            var compressedFileName = Path.GetFileNameWithoutExtension(request.File.FileName) + "_compressed.jpg";
            var compressedFilePath = Path.Combine(cacheFolder, compressedFileName);

            // 壓縮圖片並保存
            ImageUtils.CompressImage(originalFilePath, compressedFilePath);

            // 組合路徑
            var originalUrl = $"/upload/images/{tableName}/{request.File.FileName}";
            var compressedUrl = $"/upload/images/cache/{tableName}/{compressedFileName}";

            return new ImageResponseModel
            {
                OriginalUrl = originalUrl,
                CompressedUrl = compressedUrl
            };
        }
    }
}

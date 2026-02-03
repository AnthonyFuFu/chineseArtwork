using chineseArtwork.Models;

namespace chineseArtwork.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageResponseModel> SaveImageAsync(ImageUploadRequest request);
    }
}

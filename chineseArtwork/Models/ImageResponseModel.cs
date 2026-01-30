namespace chineseArtwork.Models
{
    public class ImageResponseModel
    {
        public required string OriginalUrl { get; set; }  // 原圖路徑
        public required string CompressedUrl { get; set; } // 壓縮圖路徑
    }
}

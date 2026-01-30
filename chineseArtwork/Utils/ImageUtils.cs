using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace chineseArtwork.Utils
{
    public static class ImageUtils
    {
        /// <summary>
        /// 壓縮圖片並儲存
        /// </summary>
        /// <param name="sourcePath">原始圖片路徑</param>
        /// <param name="destinationPath">壓縮後圖片儲存路徑</param>
        /// <param name="width">目標圖片的寬度（像素）</param>
        /// <param name="quality">JPEG 的壓縮品質（1-100）</param>
        public static void CompressImage(string sourcePath, string destinationPath, int width = 300, int quality = 75)
        {
            // 確保壓縮的參數有效
            if (width <= 0) throw new ArgumentException("目標寬度必須大於 0", nameof(width));
            if (quality is < 1 or > 100) throw new ArgumentException("品質需在 1 到 100 範圍內", nameof(quality));

            // 加載原始圖片
            using (var image = SixLabors.ImageSharp.Image.Load(sourcePath))
            {
                // 調整大小與壓縮圖片
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(width, 0), // 設置目標寬度，高度自動按比例計算
                    Mode = ResizeMode.Max     // 確保圖片保持比例縮放，且不裁剪溢出部分
                }));

                // 儲存圖片為壓縮的 JPG 格式
                var jpegEncoder = new JpegEncoder
                {
                    Quality = quality // 設置壓縮品質（越低越小但質量下降）
                };

                image.Save(destinationPath, jpegEncoder);
            }
        }
    }
}

namespace chineseArtwork.Models
{
    public class ImageUploadRequest
    {
        public required string TableName { get; set; } // 表格名稱（決定資料夾名稱）
        public required IFormFile File { get; set; }  // 要上傳的圖片文件
    }
}

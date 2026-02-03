using Microsoft.AspNetCore.Mvc;
using chineseArtwork.Models;
using chineseArtwork.Services.Interfaces;

namespace chineseArtwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadRequest request)
        {
            if (request.File == null || request.File.Length == 0)
                return BadRequest("A file must be provided.");

            var result = await _imageService.SaveImageAsync(request);
            return Ok(result); // 回傳圖片的壓縮圖片 URL 和原圖 URL
        }
    }
}

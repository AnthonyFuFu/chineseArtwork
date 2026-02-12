using System.ComponentModel.DataAnnotations;

namespace chineseArtwork.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "請輸入帳號")]
        [Display(Name = "帳號")]
        public string Account { get; set; } = null!;  // 改為 Account

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; } = null!;

        [Display(Name = "記住我")]
        public bool RememberMe { get; set; } = false;
    }
}

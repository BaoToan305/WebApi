using System.ComponentModel.DataAnnotations;

namespace CreateApiProject.Models
{
    public class UserDto
    {
        [Required]
        [MinLength(3, ErrorMessage ="Tài khoản phải có ít nhất 3 ký tự")]
        [MaxLength(20, ErrorMessage = "Tài khoản tối đa 20 ký tự")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Mật khẩu phải có ít nhất 5 ký tự")]
        [MaxLength(25, ErrorMessage = "Mật khẩu tối đa 25 ký tự")]
        public string Password { get; set; } = string.Empty;
    }
}

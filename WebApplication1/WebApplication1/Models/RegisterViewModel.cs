using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Kullanıcı Adı Gereklidir.")]
        [StringLength(20)]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir.")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter içermelidir.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter içermelidir.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Tekrar şifre girişi zorunludur.")]
        [MinLength(8, ErrorMessage = "Şifre en az 8 karakter içermelidir.")]
        [MaxLength(16, ErrorMessage = "Şifre en fazla 16 karakter içermelidir.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

    }

}

using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ProjectRecommendModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Başlık bilgisi gereklidir.")]
        public string? Baslik { get; set; }
        [Required(ErrorMessage = "Yıl bilgisi gereklidir.")]
        public string? Yil { get; set; }
        [Required(ErrorMessage = "Özel bilgisi gereklidir.")]
        public string? Ozet { get; set; }
        [Required(ErrorMessage = "Anahtar kelimeler bilgisi gereklidir.")]
        public string? AnahtarKelimeler { get; set; }
        public int OgrenciId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("ProjectRecommendEntity")]
    public class ProjectRecommendEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Başlık")]
        public string? Baslik { get; set; }
        [Required]
        [Display(Name = "Özet")]
        public string? Ozet { get; set; }
        [Required]
        [Display(Name = "Anahtar Kelimeler")]
        public string? AnahtarKelimeler { get; set; }
        [Required]
        [Display(Name = "Yıl")]
        public string? Yil { get; set; }
        [Display(Name = "Benzerlik Onay Durumu")]
        public bool? SimilarityOnayDurumu { get; set; } = false;
        [Display(Name = "Danışman Onay Durumu")]
        public bool? OgretmenOnayDurumu { get; set; }
        [Display(Name = "Benzerlik Skoru")]
        public int SimilarityScore { get; set; }
        [Display(Name = "Öğrenci Id")]
        public int? OgrenciId { get; set; }
        public virtual Ogrenci? Ogrenci { get; set; }
        [Display(Name = "Öğrenci Adı")]
        public string OgrenciName { get; set; }
        [Display(Name = "Danışman Yorumu")]
        public string TeacherComment { get; set; }

    }
}

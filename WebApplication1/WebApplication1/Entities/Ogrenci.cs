using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Entities
{
    [Table("Ogrenciler")]
    public class Ogrenci
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string StudentName { get; set; }

        [Required]
        [StringLength(100)]
        public string Password{ get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(20)]
        public String Role { get; set; } = "user";
        public int? ProjectRecommendId { get; set; }
        public virtual ProjectRecommendEntity? ProjectRecommend { get; set; }
    }
}


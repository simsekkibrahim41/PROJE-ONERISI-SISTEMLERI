using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Configuration
{
	public class OgrenciConfiguration : IEntityTypeConfiguration<Ogrenci>
    {
        public void Configure(EntityTypeBuilder<Ogrenci> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.ProjectRecommend).WithOne(p => p.Ogrenci).HasForeignKey<ProjectRecommendEntity>();
        }
    }
}

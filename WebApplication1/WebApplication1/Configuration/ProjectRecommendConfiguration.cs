using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.Configuration
{
	public class ProjectRecommendConfiguration : IEntityTypeConfiguration<ProjectRecommendEntity>
    {
        public void Configure(EntityTypeBuilder<ProjectRecommendEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Ogrenci).WithOne(p => p.ProjectRecommend).HasForeignKey<Ogrenci>();
        }
    }
}

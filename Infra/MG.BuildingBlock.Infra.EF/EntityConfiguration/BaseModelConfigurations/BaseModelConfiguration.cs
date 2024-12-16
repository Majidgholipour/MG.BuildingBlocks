using MG.BuildingBlock.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MG.BuildingBlock.Infra.EF.EntityConfiguration.BaseModelConfigurations
{
    public class BaseModelConfiguration<TEntity, TPrimaryKey> where TEntity 
        : BaseEntity<TPrimaryKey>
    {
        public BaseModelConfiguration()
        {
        }
        public BaseModelConfiguration(EntityTypeBuilder<TEntity> builder)
        {
            //PK
            builder.HasKey(t => t.Id);
            
            //CreateUserId
            builder
                .Property(c => c.CreatorUserId)
                .IsRequired();
            
            //CreateDate
            builder
              .Property(m => m.CreateDate)
              .IsRequired()
              .HasDefaultValueSql("getDate()");

            //CreatorUserId
            builder
              .Property(m => m.CreatorUserId)
              .IsRequired();
            
            builder.ToTable(typeof(TEntity).Name);
        }
    }
}

using _4tecture.DataAccess.EntityFramework.Storages;
using DevFun.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFun.Storage.EntityConfigurations
{
    public class CategoryConfiguration : RelationalEntityConfigurationBase<Category>
    {

        protected override void ConfigureEntity(EntityTypeBuilder<Category> entity)
        {
            // Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Name);

            // Relations

        }

        protected override void ConfigureTableMapping(EntityTypeBuilder entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("Category");
        }
    }
}

using _4tecture.DataAccess.EntityFramework.Storages;
using DevFun.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFun.Storage.EntityConfigurations
{
    public class CategoryConfiguration : RelationalEntityConfigurationBase<Category>
    {
        public CategoryConfiguration(ISchemaManager schemaManager)
            : base(schemaManager)
        {
        }

        protected override string TableName => nameof(Category);

        protected override void ConfigureEntity(EntityTypeBuilder<Category> entity)
        {
            if (entity is null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            // Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Name);

            // FK

            // Relations
        }
    }
}
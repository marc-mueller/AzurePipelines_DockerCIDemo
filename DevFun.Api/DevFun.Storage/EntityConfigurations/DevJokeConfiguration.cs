using _4tecture.DataAccess.EntityFramework.Storages;
using DevFun.Common.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevFun.Storage.EntityConfigurations
{
    public class DevJokeConfiguration : RelationalEntityConfigurationBase<DevJoke>
    {
        public DevJokeConfiguration(ISchemaManager schemaManager)
            : base(schemaManager)
        {
        }

        protected override string TableName => nameof(DevJoke);

        protected override void ConfigureEntity(EntityTypeBuilder<DevJoke> entity)
        {
            if (entity is null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            // Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Author);
            entity.Property(e => e.Text);
            entity.Property(e => e.ImageUrl);
            entity.Property(e => e.Tags);
            entity.Property(e => e.LikeCount);

            // FK

            // Relations
            entity.HasOne(e => e.Category).WithMany();
        }
    }
}
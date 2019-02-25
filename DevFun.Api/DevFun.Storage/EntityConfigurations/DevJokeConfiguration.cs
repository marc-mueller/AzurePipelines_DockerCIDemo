using _4tecture.DataAccess.EntityFramework.Storages;
using DevFun.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFun.Storage.EntityConfigurations
{
    public class DevJokeConfiguration : RelationalEntityConfigurationBase<DevJoke>
    {

        protected override void ConfigureEntity(EntityTypeBuilder<DevJoke> entity)
        {
            // Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Author);
            entity.Property(e => e.Text);
            entity.Property(e => e.ImageUrl);
            entity.Property(e => e.Tags);
            entity.Property(e => e.LikeCount);

            // Relations
            entity.HasOne(e => e.Category).WithMany();

        }

        protected override void ConfigureTableMapping(EntityTypeBuilder entityTypeBuilder)
        {
            entityTypeBuilder.ToTable("DevJoke");
        }
    }
}

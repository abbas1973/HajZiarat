using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    public class MyEntityConfig : BaseEntityConfig<MyEntity>
    {
        public override void CustomeConfigure(EntityTypeBuilder<MyEntity> builder)
        {
            builder.ToTable("MyEntities");

            #region Validations
            builder.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(300);
            #endregion
        }

    }
}

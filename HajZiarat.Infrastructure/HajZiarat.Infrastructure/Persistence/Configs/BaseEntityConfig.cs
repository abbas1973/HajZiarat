using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence
{
    public abstract class BaseEntityConfig<TEntity> : 
        IEntityTypeConfiguration<TEntity> 
        where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //#region Properties
            //builder.Property(e => e.CreatedBy)
            //        .HasMaxLength(100);

            //builder.Property(e => e.LastModifiedBy)
            //    .HasMaxLength(100);
            //#endregion

            CustomeConfigure(builder);
        }

        /// <summary>
        /// فانکشنی که باید توسط بقیه کلاس های کانفیگ پیاده سازی شود
        /// </summary>
        /// <param name="builder"></param>
        public abstract void CustomeConfigure(EntityTypeBuilder<TEntity> builder);
    }
}

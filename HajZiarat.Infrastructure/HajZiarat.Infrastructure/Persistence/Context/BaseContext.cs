using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Domain.Entities;
using Base.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence
{
    public class BaseContext : DbContext
    {
        #region Private fields
        protected Assembly _domainAssembly { get; }
        protected Assembly _infrastructureAssembly { get; }
        #endregion


        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">آپشن های دیتابیس</param>
        /// <param name="accessor">اکسسور</param>
        /// <param name="domainAssembly">اسمبلی دامین</param>
        /// <param name="infrastructureAssembly">اسمبلی اینفراستراکچر</param>
        public BaseContext(
            DbContextOptions options,
            IHttpContextAccessor accessor,
            Assembly domainAssembly, 
            Assembly infrastructureAssembly)
            : base(options)
        {
            _domainAssembly = domainAssembly;
            _infrastructureAssembly = infrastructureAssembly;
        }
        #endregion



        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Fluent API - لود کردن از کلاس های جانبی
            modelBuilder.ApplyConfigurationsFromAssembly(_infrastructureAssembly);
            #endregion


            #region DeleteBehavior - رفتار در هنگام حذف دیتا
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            #endregion


            #region رجیستر کردن همه DBSet ها
            modelBuilder.RegisterAllEntities<BaseEntity>(_domainAssembly);
            #endregion
        } 
        #endregion



        #region SaveChanges Override
        #region SaveChangesAsync
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetCreatorAndModifier();
            var res = base.SaveChangesAsync(cancellationToken);
            return res;
        }
        #endregion


        #region SaveChanges
        public override int SaveChanges()
        {
            SetCreatorAndModifier();
            var res = base.SaveChanges();
            return res;
        }
        #endregion


        #region مقدار دهی اطلاعات ایجاد و ویرایش
        public void SetCreatorAndModifier()
        {
            foreach (var entry in ChangeTracker.Entries<IBaseEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreateDate = DateTime.Now;
                else if (entry.State == EntityState.Modified)
                    entry.Entity.ModifyDate = DateTime.Now;
            }
        }
        #endregion


        #endregion



    }

}


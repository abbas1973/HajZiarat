using Application.Exceptions;
using Application.Repositories;
using DocumentFormat.OpenXml.InkML;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class Repository<TEntity> : BaseRepository<TEntity>, IRepository<TEntity>
    where TEntity : class
    {
        public Repository(DbContext _Context) : base(_Context)
        {
        }


        #region توابع مستقیم برای اعمال روی دیتابیس بدون نیاز به کامیت
        #region Bulk Insert
        public bool BulkInsert(List<TEntity> entities)
        {
            try
            {
                //DbContextBulkExtensions.BulkInsert<TEntity>(_context, entities);
                _context.BulkInsert(entities);
                return true;
            }
            catch (Exception e)
            {
                throw new CustomSqlException("بالک اینسرت با خطا همراه بوده است!", e);
            }
        }


        public async Task<bool> BulkInsertAsync(List<TEntity> entities)
        {
            try
            {
                //DbContextBulkExtensions.BulkInsert<TEntity>(_context, entities);
                await _context.BulkInsertAsync(entities);
                return true;
            }
            catch (Exception e)
            {
                throw new CustomSqlException("بالک اینسرت با خطا همراه بوده است!", e);
            }
        }
        #endregion


        #region آپدیت مستقیم روی دیتابیس بدون نیاز به کامیت
        /// <summary>
        /// آپدیت مستقیم روی دیتابیس بدون نیاز به کامیت
        /// <para>example : </para>
        /// <para>
        /// ExecuteUpdate(x => x.Id == 1, x => x.SetProperty(z => z.IsEnabled, z => true))
        /// </para>
        /// </summary>
        /// <param name="filter">شرط برای رکوردهایی که باید ویرایش شوند</param>
        /// <param name="updateExpression">فیلدهایی که باید آپدیت شوند</param>
        /// <returns>تعداد آیتم های آپدیت شده</returns>
        public virtual int ExecuteUpdate(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
        {
            return _entities.Where(filter).ExecuteUpdate(updateExpression);
        }


        /// <summary>
        /// آپدیت مستقیم روی دیتابیس بدون نیاز به کامیت
        /// <para>example : </para>
        /// <para>
        /// ExecuteUpdate(x => x.Id == 1, x => x.SetProperty(z => z.IsEnabled, z => true))
        /// </para>
        /// </summary>
        /// <param name="filter">شرط برای رکوردهایی که باید ویرایش شوند</param>
        /// <param name="updateExpression">فیلدهایی که باید آپدیت شوند</param>
        /// <returns>تعداد آیتم های آپدیت شده</returns>
        public virtual async Task<int> ExecuteUpdateAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression)
        {
            return await _entities.Where(filter).ExecuteUpdateAsync(updateExpression);
        }
        #endregion



        #region حذف مستقیم روی دیتابیس بدون نیاز به کامیت
        /// <summary>
        /// حذف مستقیم روی دیتابیس بدون نیاز به کامیت بصورت آسنکرون
        /// <para>example : </para>
        /// <para>
        /// ExecuteDelete(x => x.Id == 1)
        /// </para>
        /// </summary>
        /// <param name="filter">شرط برای رکوردهایی که باید حذف شوند</param>
        /// <returns>تعداد آیتم های حذف شده</returns>
        public virtual int ExecuteDelete(Expression<Func<TEntity, bool>> filter)
        {
            return _entities.Where(filter).ExecuteDelete();
        }


        /// <summary>
        /// حذف مستقیم روی دیتابیس بدون نیاز به کامیت بصورت آسنکرون
        /// <para>example : </para>
        /// <para>
        /// ExecuteDelete(x => x.Id == 1)
        /// </para>
        /// </summary>
        /// <param name="filter">شرط برای رکوردهایی که باید حذف شوند</param>
        /// <returns>تعداد آیتم های حذف شده</returns>
        public virtual async Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _entities.Where(filter).ExecuteDeleteAsync();
        }
        #endregion

        #endregion


    }



}

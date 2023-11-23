using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IRepository<TEntity> : IBaseRepository<TEntity>, IReadOnlyRepository<TEntity> where TEntity : class
    {
        #region توابع مستقیم برای اعمال روی دیتابیس بدون نیاز به کامیت

        #region Bulk Insert
        bool BulkInsert(List<TEntity> entities);
        Task<bool> BulkInsertAsync(List<TEntity> entities);
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
        int ExecuteUpdate(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression);


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
        Task<int> ExecuteUpdateAsync(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> updateExpression);
        #endregion




        #region حذف بدون نیاز به کامیت
        int ExecuteDelete(Expression<Func<TEntity, bool>> filter);

        
        Task<int> ExecuteDeleteAsync(Expression<Func<TEntity, bool>> filter);
        #endregion
        #endregion

    }


}

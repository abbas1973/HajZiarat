using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Application.Repositories;

namespace Infrastructure.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class BaseUnitOfWork : IUnitOfWorkBase
    {
        protected DbContext _context { get; }
        public BaseUnitOfWork(DbContext context)
        {
            _context = context;
        }


        #region Commit
        /// <summary>
        /// ذخیره تغییرات انجام شده
        /// </summary>
        /// <returns></returns>
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new CustomSqlException("ذخیره سازی اطلاعات با خطا همراه بوده است!", ex);
            }
        }



        /// <summary>
        /// ذخیره تغییرات انجام شده
        /// </summary>
        /// <returns></returns>
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new CustomSqlException("ذخیره سازی اطلاعات با خطا همراه بوده است!", ex);            
            }
        }
        #endregion


        #region ExecuteNonQuery

        /// <summary>
        /// اجرای یک کوئری بدون مقدار بازگشتی
        /// </summary>
        /// <param name="Query">متن کوئری</param>
        /// <param name="Parameters">پارامتر های کوئری</param>
        /// <returns></returns>
        public bool ExecuteNonQuery<TContext>(string Query, params object[] Parameters)
        {
            try
            {
                int NumberOfRowEffected = _context.Database.ExecuteSqlRaw(Query, Parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new CustomSqlException("اجرای دستور با خطا همراه بوده است!", ex);
            }
        }



        /// <summary>
        /// اجرای یک کوئری بدون مقدار بازگشتی
        /// </summary>
        /// <param name="Query">متن کوئری</param>
        /// <param name="Parameters">پارامتر های کوئری</param>
        /// <returns></returns>
        public async Task<bool> ExecuteNonQueryAsync<TContext>(string Query, params object[] Parameters)
        {
            try
            {
                int NumberOfRowEffected = await _context.Database.ExecuteSqlRawAsync(Query, Parameters);
                return true;
            }
            catch (Exception ex)
            {
                throw new CustomSqlException("اجرای دستور با خطا همراه بوده است!", ex);
            }
        }

        #endregion


        /// <summary>
        /// پاک کردن آبجکت unit of work و Context از رم
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }


    }
}

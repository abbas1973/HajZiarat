using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Application.Repositories;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _entities;

        public ReadOnlyRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }



        #region اعمال شرط ها و جوین ها و مرتب سازی و صفحه بندی

        /// <summary>
        /// اعمال شرط ها و مرتب سازی و صفحه بندی
        /// <para>
        /// GetQueryable(x => x.id == 1 , x => x.OrderBy(a => a.id).ThenByDescending(a => a.Amount) , 5, 2)
        /// </para>
        /// </summary>
        /// <param name="filter">شرط</param>
        /// <param name="orderBy">مرتب سازی</param>
        /// <param name="skip">تعداد آیتم هایی که باید عبور شود</param>
        /// <param name="take">تعداد آیتم هایی که باید گرفته شود</param>
        /// <returns>کوئری از آیتم ها</returns>
        private IQueryable<TEntity> GetQueryable(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true
            )
        {
            var query = _entities.AsQueryable();
            if (disableTracking) 
                query = query.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            return query;
        }




        /// <summary>
        /// اعمال جوین ها
        /// </summary>
        /// <param name="includes">جوین ها</param>
        /// <returns>کوئری از آیتم ها</returns>
        private IQueryable<TEntity> GetIncluds(
            IQueryable<TEntity> query,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )
        {
            if (includes != null)
            {
                query = includes(query);
            }
            return query;
        }
        #endregion




        #region GetAll
        /// <summary>
        /// گرفتن همه موجودیت ها 
        /// </summary>
        /// <returns></returns>
        public virtual List<TEntity> GetAll(bool disableTracking = true)
        {
            var query = _entities.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            return query.ToList();
        }

        /// <summary>
        /// گرفتن همه موجودیت ها 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAllAsync(bool disableTracking = true)
        {
            var query = _entities.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            var model = await query.ToListAsync();
            return model;
        }
        #endregion




        #region Get
        /// <summary>
        /// جستجو بین آیتم ها
        /// </summary>
        /// <param name="filter">عبارت جستجو</param>
        /// <param name="orderBy">عبارت مرتب سازی</param>
        /// <param name="skip">تعداد آیتم هایی که باید از آن عبور شود</param>
        /// <param name="take">تعداد آیتم هایی که باید برگردد</param>
        /// <param name="include">وابستگی ها و جوین ها</param>
        /// <returns></returns>
        public virtual List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true
            )
        {
            return GetQueryable(filter, orderBy, skip, take, includes, disableTracking).ToList();
        }


        /// <summary>
        /// جستجو بین آیتم ها
        /// </summary>
        /// <param name="filter">عبارت جستجو</param>
        /// <param name="orderBy">عبارت مرتب سازی</param>
        /// <param name="skip">تعداد آیتم هایی که باید از آن عبور شود</param>
        /// <param name="take">تعداد آیتم هایی که باید برگردد</param>
        /// <param name="include">وابستگی ها و جوین ها</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true
            )
        {
            var model = await GetQueryable(filter, orderBy, skip, take, includes, disableTracking).ToListAsync();
            return model;
        }
        #endregion




        #region GetDTO
        /// <summary>
        /// گرفتن لیستی از مدل دلخواه
        /// </summary>
        /// <typeparam name="TResult">نوع مدل بازگشتی</typeparam>
        /// <param name="selector">انتخاب کننده برای پروپرتی های مورد نظر</param>
        /// <param name="filter">شرط ها</param>
        /// <param name="orderBy">مرتب سازی</param>
        /// <param name="skip">تعداد ایتم هایی که باید از آنها عبور شود</param>
        /// <param name="take">تعداد ایتم هایی که باید در خروجی برگردد</param>
        /// <param name="include">وابستگی ها و جوین ها</param>
        /// <returns>لیستی از مدل دلخواه</returns>
        public virtual List<TResult> GetDTO<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true
            )
        {
            return GetQueryable(filter, orderBy, skip, take, include, disableTracking)
                            .Select(selector).ToList();
        }



        /// <summary>
        /// گرفتن لیستی از مدل دلخواه
        /// </summary>
        /// <typeparam name="TResult">نوع مدل بازگشتی</typeparam>
        /// <param name="selector">انتخاب کننده برای پروپرتی های مورد نظر</param>
        /// <param name="filter">شرط ها</param>
        /// <param name="orderBy">مرتب سازی</param>
        /// <param name="skip">تعداد ایتم هایی که باید از آنها عبور شود</param>
        /// <param name="take">تعداد ایتم هایی که باید در خروجی برگردد</param>
        /// <param name="include">وابستگی ها و جوین ها</param>
        /// <returns>لیستی از مدل دلخواه</returns>
        public virtual async Task<List<TResult>> GetDTOAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            bool disableTracking = true
            )
        {
            return await GetQueryable(filter, orderBy, skip, take, include, disableTracking)
                            .Select(selector).ToListAsync();
        }
        #endregion



        #region GetOneDTO
        /// <summary>
        /// گرفتن یک مدل دلخواه
        /// </summary>
        /// <typeparam name="TResult">نوع مدل بازگشتی</typeparam>
        /// <param name="selector">انتخاب کننده برای پروپرتی های مورد نظر</param>
        /// <param name="filter">شرط ها</param>
        /// <param name="includes">وابستگی ها و جوین ها</param>
        /// <returns>یک مدل دلخواه</returns>
        public TResult GetOneDTO<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            )
        {
            return GetQueryable(filter, orderBy, null, null, include)
                            .Select(selector)
                            .FirstOrDefault();
        }



        /// <summary>
        /// گرفتن یک مدل دلخواه بصورت آسنکرون
        /// </summary>
        /// <typeparam name="TResult">نوع مدل بازگشتی</typeparam>
        /// <param name="selector">انتخاب کننده برای پروپرتی های مورد نظر</param>
        /// <param name="filter">شرط ها</param>
        /// <param name="includes">وابستگی ها و جوین ها</param>
        /// <returns>یک مدل دلخواه</returns>
        public async Task<TResult> GetOneDTOAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null
            )
        {
            return await GetQueryable(filter, orderBy, null, null, includes)
                            .Select(selector)
                            .FirstOrDefaultAsync();
        }
        #endregion




        #region SingleOrDefault
        /// <summary>
        /// گرفتن یک موجودیت به همراه وابستگی ها
        /// </summary>
        /// <param name="filter">شرط ها</param>
        /// <param name="includes">وابستگی ها و جوین ها</param>
        /// <returns></returns>
        public virtual TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )

        {
            var temp = GetQueryable(filter);
            return GetIncluds(temp, includes).SingleOrDefault();
        }

        /// <summary>
        /// گرفتن یک موجودیت به همراه وابستگی ها بصورت آسنکرون
        /// </summary>
        /// <param name="filter">شرط ها</param>
        /// <param name="includes">وابستگی ها و جوین ها</param>
        /// <returns></returns>
        public virtual async Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )
        {
            var temp = GetQueryable(filter);
            return await GetIncluds(temp, includes).SingleOrDefaultAsync();
        }
        #endregion



        #region FirstOrDefault
        /// <summary>
        /// گرفتن اولین موجودیت به همراه وابستگی ها پس از مرتب سازی
        /// </summary>
        /// <param name="filter">شرط ها</param>
        /// <param name="orderBy">مرتب سازی</param>
        /// <param name="includes">وابستگی ها و جوین ها</param>
        /// <returns></returns>
        public virtual TEntity FirstOrDefault(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )
        {
            var temp = GetQueryable(filter, orderBy);
            return GetIncluds(temp, includes).FirstOrDefault();
        }

        /// <summary>
        /// گرفتن اولین موجودیت به همراه وابستگی ها پس از مرتب سازی
        /// </summary>
        /// <param name="filter">شرط ها</param>
        /// <param name="orderBy">مرتب سازی</param>
        /// <param name="includes">وابستگی ها و جوین ها</param>
        public virtual async Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null
            )

        {
            var temp = GetQueryable(filter, orderBy);
            return await GetIncluds(temp, includes).FirstOrDefaultAsync();
        }
        #endregion



        #region GetById
        public virtual TEntity GetById(object id)
        {
            var entity = _entities.Find(id);
            return entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await _entities.FindAsync(id);
            return entity;
        }
        #endregion



        #region Count
        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetQueryable(filter).CountAsync();
        }
        #endregion



        #region Sum
        public virtual int Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, int>> selector = null)
        {
            return GetQueryable(filter).Sum(selector);
        }
        public virtual decimal Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, decimal>> selector = null)
        {
            return GetQueryable(filter).Sum(selector);
        }
        public virtual float Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, float>> selector = null)
        {
            return GetQueryable(filter).Sum(selector);
        }
        public virtual double Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, double>> selector = null)
        {
            return GetQueryable(filter).Sum(selector);
        }

        public virtual async Task<int> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, int>> selector = null)
        {
            return await GetQueryable(filter).SumAsync(selector);
        }
        public virtual async Task<decimal> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, decimal>> selector = null)
        {
            return await GetQueryable(filter).SumAsync(selector);
        }
        public virtual async Task<float> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, float>> selector = null)
        {
            return await GetQueryable(filter).SumAsync(selector);
        }
        public virtual async Task<double> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, double>> selector = null)
        {
            return await GetQueryable(filter).SumAsync(selector);
        }
        #endregion



        #region Max
        public virtual TResult Max<TResult>(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, TResult>> selector = null)
        {
            return GetQueryable(filter).Max(selector);
        }

        public virtual async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, TResult>> selector = null)
        {
            return await GetQueryable(filter).MaxAsync(selector);
        }
        #endregion



        #region Any
        public virtual bool Any(Expression<Func<TEntity, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return await GetQueryable(filter).AnyAsync();
        }
        #endregion

    }
}

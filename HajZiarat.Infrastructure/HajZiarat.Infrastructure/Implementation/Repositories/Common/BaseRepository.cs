using Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class BaseRepository<TEntity> : ReadOnlyRepository<TEntity>, IBaseRepository<TEntity>
    where TEntity : class
    {
        public BaseRepository(DbContext context) : base(context)
        {
        }



        #region افزودن یک موجودیت جدید
        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }
        #endregion




        #region افزودن موجویت های جدید
        /// <summary>
        /// ایجاد چند موجودیت جدید
        /// </summary>
        /// <param name="entities">لیستی از موجودیت ها</param>
        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        /// <summary>
        /// ایجاد چند موجودیت جدید بصورت آسنکرون
        /// </summary>
        /// <param name="entities">لیستی از موجودیت ها</param>
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entities.AddRangeAsync(entities);
        }
        #endregion




        #region ویرایش موجودیت
        /// <summary>
        /// ویرایش  کل موجودیت یا چند فیلد خاص از یک موجودیت
        /// <para>
        /// var book = GetById(1);
        /// book.Title = "My new title";
        /// repository.Update(book, b => b.Title);
        /// </para>
        /// </summary>
        /// <param name="entity">موجودیت مورد نظر</param>
        /// <param name="UpdatedProperties">فیلد هایی که اپدیت شدند</param>
        public virtual void Update(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
                _entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        #endregion





        #region حذف موجودیت
        /// <summary>
        /// حذف موجودیت با آیدی
        /// </summary>
        /// <param name="id">آیدی موجودیت</param>
        public virtual void Remove(object id)
        {
            if (id == null) return;
            var entity = _entities.Find(id);
            Remove(entity);
        }


        /// <summary>
        /// حذف موجودیت
        /// </summary>
        /// <param name="entity">موجودیت</param>
        public virtual void Remove(TEntity entity)
        {
            if (entity == null) return;
            if (_context.Entry(entity).State == EntityState.Detached)
                _entities.Attach(entity);
            _entities.Remove(entity);
        }


        /// <summary>
        /// حذف چند موجودیت
        /// </summary>
        /// <param name="entities">لیستی از موجودیت ها</param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        #endregion



    }



}

namespace Application.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        #region افزودن موجودیت جدید
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        #endregion


        #region افزودن موجودیت های جدید
        void AddRange(IEnumerable<TEntity> entities);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
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
        void Update(TEntity entity);
        #endregion


        #region حذف موجودیت
        void Remove(object id);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        #endregion


    }


}

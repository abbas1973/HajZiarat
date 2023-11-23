using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Repositories
{
    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IReadOnlyRepository<TEntity>
    {
        #region GetAll
        List<TEntity> GetAll(bool disableTracking = true);
        Task<List<TEntity>> GetAllAsync(bool disableTracking = true);
        #endregion


        #region Get
        List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true);


        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true);
        #endregion


        #region GetDTO
        List<TResult> GetDTO<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true);

        Task<List<TResult>> GetDTOAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            int? skip = null,
            int? take = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            bool disableTracking = true);
        #endregion


        #region GetOneDTO
        TResult GetOneDTO<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task<TResult> GetOneDTOAsync<TResult>(
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        #endregion


        #region SingleOrDefault
        TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        #endregion


        #region FirstOrDefault
        TEntity FirstOrDefault(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);

        Task<TEntity> FirstOrDefaultAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null);
        #endregion


        #region GetById
        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);
        #endregion



        #region Count
        int Count(Expression<Func<TEntity, bool>> filter = null);

        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
        #endregion


        #region Sum
        int Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, int>> selector = null);
        decimal Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, decimal>> selector = null);
        float Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, float>> selector = null);
        double Sum(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, double>> selector = null);

        Task<int> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, int>> selector = null);
        Task<decimal> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, decimal>> selector = null);
        Task<float> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, float>> selector = null);
        Task<double> SumAsync(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, double>> selector = null);
        #endregion


        #region Max
        TResult Max<TResult>(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, TResult>> selector = null);

        Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, bool>> filter = null,
                               Expression<Func<TEntity, TResult>> selector = null);
        #endregion



        #region Any
        bool Any(Expression<Func<TEntity, bool>> filter = null);

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter = null); 
        #endregion
    }
}

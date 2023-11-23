using Microsoft.EntityFrameworkCore;
using Application.Exceptions;
using Application.Repositories;
using Domain.Entities;

namespace Infrastructure.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public class GenericBaseUnitOfWork<TEntity> : BaseUnitOfWork, IGenericBaseUnitOfWork<TEntity>
        where TEntity: class
    {
        public GenericBaseUnitOfWork(DbContext context): base(context)
        {
        }



        #region User 
        private IRepository<TEntity> _repository;
        public IRepository<TEntity> Repository
        {
            get
            {
                if (_repository == null)
                    _repository = new Repository<TEntity>(_context);
                return _repository;
            }
        }
        #endregion


    }
}

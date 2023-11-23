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
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        public UnitOfWork(DbContext context) : base(context)
        {
        }


        #region Repositories

        #region MyEntity 
        private IMyEntityRepository _myEntities;
        public IMyEntityRepository MyEntities
        {
            get
            {
                if (_myEntities == null)
                    _myEntities = new MyEntityRepository(_context);
                return _myEntities;
            }
        }
        #endregion

        #endregion




    }
}

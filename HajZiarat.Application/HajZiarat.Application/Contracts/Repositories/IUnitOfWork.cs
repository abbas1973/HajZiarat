﻿namespace Application.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IUnitOfWork : IUnitOfWorkBase, IDisposable
    {
        #region Entities
        IMyEntityRepository MyEntities { get; }        
        #endregion
    }
}

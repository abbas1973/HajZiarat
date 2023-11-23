namespace Application.Repositories
{

    /// <summary>
    /// POWERED BY Abbas MohammadNezhad
    /// <para>
    /// email: abbas.mn1973@gmail.com
    /// </para>
    /// </summary>
    public interface IGenericBaseUnitOfWork<TEntity> : IUnitOfWorkBase
        where TEntity : class
    {
        IRepository<TEntity> Repository { get; }
    }
}

using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Persistence
{
    public class ApplicationContext : BaseContext
    {
        #region Constructors
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor accessor)
            : base(options, accessor, typeof(MyEntity).Assembly, typeof(ApplicationContext).Assembly) { }
        #endregion


        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }

}

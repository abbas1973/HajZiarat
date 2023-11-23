using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Application.Repositories;

namespace Infrastructure.Repositories
{
    public class MyEntityRepository : Repository<MyEntity>, IMyEntityRepository
    {
        public MyEntityRepository(DbContext context) : base(context)
        {
        }

    }
}

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Base.Infrastructure.Persistence
{
    public static class RegisterEntitiesExtension
    {
        /// <summary>
        ///  رجیستر کردن همه انتیتی ها از نوع BaseEntity به عنوان DbSet های کانتکست
        /// </summary>
        /// <typeparam name="BaseType"></typeparam>
        /// <param name="modelBuilder"></param>
        /// <param name="assemblies"></param>
        public static void RegisterAllEntities<BaseType>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(a => a.GetExportedTypes())
                                  .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && typeof(BaseType).IsAssignableFrom(c));

            foreach (var type in types)
                modelBuilder.Entity(type);
        }
    }
}

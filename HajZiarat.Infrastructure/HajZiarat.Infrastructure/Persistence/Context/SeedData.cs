using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{

    public static class SeedData
    {

        #region مایگریت کردن و مقدار دهی اولیه دیتابیس ها
        public static void SeedDatabases(this IApplicationBuilder app)
        {
            app.InitApplicationDb();
        }
        #endregion



        #region مقدار دهی اولیه به دیتابیس اپلیکیشن و آیدنتیتی
        public static void InitApplicationDb(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                #region migrate databases
                var applicationContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                applicationContext.Database.Migrate();
                #endregion

                applicationContext.SaveChanges();

            }
        }
        #endregion



    }
}

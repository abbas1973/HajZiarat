using Application.Middleware;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Etehadie.Registration
{
    public static class ConfigureMiddlewares
    {
        /// <summary>
        /// استفاده از میدلور های کاستوم توسط app
        /// IApplicationBuilder
        /// </summary>
        public static IApplicationBuilder UseCustomMiddlewares(this WebApplication app)
        {
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseErrorHandlerMiddleware();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.routeprefix = "docs";
                //c.DocExpansion(DocExpansion.None);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "حج الزیارت");
            });
            #endregion


            app.UseAuthorization();
            app.UseCors("MyPolicy");
            app.MapControllers();
            return app;
        }
    }
}

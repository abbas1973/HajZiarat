using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTopologySuite.Mathematics;

namespace Etehadie.Registration
{
    public static class ServiceRegistration
    {
        public static void RegisterServices(
            this WebApplicationBuilder builder,
            IConfiguration configuration)
        {
            var services = builder.Services;

            #region رجیستر سرویس های پایه

            #region AddControllersWithViews
            services.AddControllers();
            #endregion

            //گرفتن httpcontext در کلاس لایبرری ها
            services.AddHttpContextAccessor();

            // Cors
            services.AddCors(configuration);

            // کانفیگ swagger
            services.AddEndpointsApiExplorer();
            services.AddSwagger();
            services.AddApiVersioning();          
            #endregion


            // context و uow
            services.RegisterInfrastructure(configuration);


            // کانفیگ های لایه اپلیکیشن
            builder.RegisterApplication(configuration);
        }


        #region Cors
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsAllowed = configuration.GetSection("CorsAllowed").Get<string[]>().ToList();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials()
                  .SetIsOriginAllowed(origin =>
                  {
                      if (string.IsNullOrWhiteSpace(origin)) return false;

                      if (corsAllowed == null || !corsAllowed.Any())
                          return true;

                      return corsAllowed.Any(url => origin.ToLower() == url || origin.ToLower().StartsWith(url + "/"));
                  });
            }));
        }
        #endregion



        #region swagger
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "حج الزیارت",
                    Description = "آپلود فایل اکسل حج الزیارت",
                    TermsOfService = new Uri("https://hajziarat.ir")
                });



                #region نمایش نام گروه ها(کنترلرها) در سوگر
                c.TagActionsBy(api =>
                {
                    if (api.GroupName != null)
                    {
                        return new[] { api.GroupName };
                    }

                    var controllerActionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    if (controllerActionDescriptor != null)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });
                #endregion



                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }
        #endregion


    }
}


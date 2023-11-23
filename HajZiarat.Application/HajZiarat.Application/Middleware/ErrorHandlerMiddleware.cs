using Application.DTOs;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Utilities;

namespace Application.Middleware
{
    #region میدلور برای مدیریت خطاها
    /// <summary>
    /// میدلور برای مدیریت خطاها
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                List<string> logItems = new List<string>();
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new BaseResult(ex);
                switch (ex)
                {
                    case ValidationException e:
                        responseModel.Errors = e.Errors;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case BadRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case UnAuthorizedException e:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    case XssException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ForbiddenException e:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;
                    case CustomSqlException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    case ExternalServiceException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        logItems.Add($"Service name : {e.ServiceName}");
                        logItems.Add($"Fucntion: {e.Function}");
                        break;
                    case UserNotVerifiedException e:
                        responseModel.Errors = new List<string>() { "کاربر گرامی، ابتدا تلفن همراه خود را تایید کنید!" };
                        response.StatusCode = (int)HttpStatusCode.Locked;
                        break;
                    case InternalServerException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;


                    case FileException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    case FileNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    case DirectoryNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;


                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Errors = new List<string>() { "خطای سرور رخ داده است. مجددا اقدام کنید." };
                        break;
                }

                #region لاگ ارور
                string exType = ex.GetType().ToString();
                logItems.Add($"ExceptionType: {exType}");
                _logger.LogError(ex, $"{ex.Message} - {string.Join(" | ", logItems)}");
                #endregion

                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
    #endregion



    #region middleware use extension
    public static class ErrorHandlerMiddlewareExtension
    {
        public static IApplicationBuilder UseErrorHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
    #endregion
}

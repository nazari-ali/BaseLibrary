using BaseLibrary.Api;
using BaseLibrary.Tool.Exceptions;
using BaseLibrary.Tool.Exceptions.Helper;
using BaseLibrary.Tool.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BaseLibrary.Middlewares
{
    public class SuperExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<SuperExceptionHandlerMiddleware> _logger;

        public SuperExceptionHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<SuperExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError;
            string message = null;

            try
            {
                await _next(httpContext);
            }
            catch (SuperException exception)
            {
                _logger.LogError(exception, exception.Message);
                httpStatusCode = exception.HttpStatusCode;

                if (_env.IsDevelopment())
                {
                    message = exception.SystemMessage;
                }
                else
                {
                    message = exception.ClientMessage;
                }

                await WriteToResponseAsync();
            }
            catch (SecurityTokenExpiredException exception)
            {
                _logger.LogError(exception, exception.Message);
                httpStatusCode = HttpStatusCode.Unauthorized;
                message = LocalExceptionMessage.UnauthorizedMessage;

                if (_env.IsDevelopment())
                {
                    message = exception.GetNormalizedException();
                    message += $"\n Expires: \n {exception.Expires.ToString()}";
                }
                    
                await WriteToResponseAsync();
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, exception.Message);
                httpStatusCode = HttpStatusCode.Unauthorized;
                message = LocalExceptionMessage.UnauthorizedMessage;

                if (_env.IsDevelopment())
                {
                    message = exception.GetNormalizedException();
                }

                await WriteToResponseAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                message = LocalExceptionMessage.InternalServerErrorMessage;

                if (_env.IsDevelopment())
                {
                    message = exception.GetNormalizedException();
                }

                await WriteToResponseAsync();
            }

            async Task WriteToResponseAsync()
            {
                if (httpContext.Response.HasStarted)
                {
                    throw new InvalidOperationException();
                }

                var result = new ApiResponse(false, httpStatusCode, message);
                var json = result.Serialize();

                httpContext.Response.StatusCode = (int)httpStatusCode;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
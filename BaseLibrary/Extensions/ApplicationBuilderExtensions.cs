using BaseLibrary.Middlewares;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseSuperExtentionHandler(
            this IApplicationBuilder app
        )
        {
            app.UseMiddleware<SuperExceptionHandlerMiddleware>();
        }
    }
}

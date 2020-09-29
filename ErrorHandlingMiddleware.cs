using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GameWebApi{

    public class ErrorHandlingMiddleware{


        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext context){
            try{
                await _next(context);
            }
            catch(NotFoundException e){
                context.Response.StatusCode = 404;
            }
        }


    }

    public static class MyMiddlewareExtensions {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder){
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
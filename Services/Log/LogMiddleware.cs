using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using BookCatalogAPI.Logs;
using Serilog;

namespace BookCatalogAPI
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;

        public LogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            //REQUEST
            context.Request.EnableBuffering();
            var requestBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            //RESPONSE
            var originalBody = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context); //exe

            memStream.Position = 0;
            var responseBody = await new StreamReader(memStream).ReadToEndAsync();
            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;

            var elapsed = (DateTime.UtcNow - startTime).TotalMilliseconds;
            
            if (context.Response.StatusCode >= 400)
            {
                Log.Error("{@RequestLog}", new
                {
                    Method = context.Request.Method,
                    Path = context.Request.Path,
                    StatusCode = context.Response.StatusCode,
                    ElapsedMs = elapsed,
                    RequestBody = requestBody,
                    responseBody = responseBody
                });
            }
            else
            {
                Log.Information("{@RequestLog}", new
                {
                    Method = context.Request.Method,
                    Path = context.Request.Path,
                    StatusCode = context.Response.StatusCode,
                    ElapsedMs = elapsed,
                    Timestamp = DateTime.UtcNow
                });
            }

            //log files
            LogTxt.LogToFile(
                $"{context.Request.Method} {context.Request.Path}", 
                $"{requestBody}", 
                context.Response.StatusCode, 
                $"{responseBody}"
            );


        }
    }
}
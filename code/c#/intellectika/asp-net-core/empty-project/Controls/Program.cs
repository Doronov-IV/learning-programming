global using System;
global using Newtonsoft.Json;
using System.Text.Json;
using emptyproject.Applications;
using emptyproject.MiddlewareLike.FileUpload;
using Microsoft.AspNetCore.Mvc;

namespace emptyproject.Controls
{
    [DisableRequestSizeLimit]
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            app.UseUploadFile();

            await app.RunAsync();
        }
    }
}
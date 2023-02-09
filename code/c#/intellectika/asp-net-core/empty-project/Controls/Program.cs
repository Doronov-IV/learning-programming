global using System;
global using Newtonsoft.Json;
using System.Text.Json;
using emptyproject.Applications;

namespace emptyproject.Controls
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            ICustomApplication listApp = new PersonListApplication(app);
            await listApp.RunAsync();

            await app.RunAsync();
        }
    }
}
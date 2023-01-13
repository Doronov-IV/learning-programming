global using System;
global using Newtonsoft.Json;
using System.Text.Json;

namespace emptyproject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");


            PersonListApplication listApp = new(app);
            listApp.RunCustomApplication();

            app.Run();
        }
    }
}
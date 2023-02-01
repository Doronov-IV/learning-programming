global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.SqlServer;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using Spectre.Console;

global using Newtonsoft.Json;

global using Microsoft.Extensions;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using MainEntityProject.Model.Entities;
global using MainEntityProject.Controls.Applications;

namespace MainEntityProject.Controls.Common
{
    public static class Program
    {

        public static IServiceCollection serviceCollection = new ServiceCollection();


        public static async Task Main(string[] args)
        {
            using var provider = serviceCollection.BuildServiceProvider();
            var app = provider.GetRequiredService<TankFactoryApplication>();
            await app.Start();
        }


        static Program()
        {
            serviceCollection.AddSingleton<TankFactoryApplication>();
            serviceCollection.AddSingleton<OwnedPropertyInheritanceApplication>();
        }
    }
}
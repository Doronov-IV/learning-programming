global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Linq;
global using System.Text;
global using System;
using Tools.Toolbox;
using Tools.Formatting;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using ShootingRangeProject;

namespace ShootingRange
{
    public static class Program
    {
        public static void Main(string[] args)
        {

            var services = new ServiceCollection();

            services.AddScoped<ILogService, WhiteConsoleLogService>();
            services.AddScoped<ILogService, GreenConsoleLogService>();
            services.AddScoped<ILogService, RedConsoleLogService>();
            services.AddScoped<Logger>();

            using var provider = services.BuildServiceProvider();

            var logService = provider.GetService<Logger>();

            logService?.Log("Hello world");

        }

    }
}
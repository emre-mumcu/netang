using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.App_Lib.Configuration.Services
{
    public static class _AutoMapper
    {
        public static IServiceCollection _AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        // public static IApplicationBuilder _UseAutoMapper(this WebApplication app)
        // {
        //     return app;
        // }
    }
}
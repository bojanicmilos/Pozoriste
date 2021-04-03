using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pozoriste.API.TokenServiceExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOpenApi(this IServiceCollection services)
        {
            services.AddOpenApiDocument();
        }
    }
}

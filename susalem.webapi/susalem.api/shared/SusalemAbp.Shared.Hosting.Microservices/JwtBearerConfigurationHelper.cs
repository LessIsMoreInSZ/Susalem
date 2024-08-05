using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Volo.Abp.Modularity;

namespace SusalemAbp.Shared.Hosting.Microservices
{
    public class JwtBearerConfigurationHelper
    {
        public static void Configure(
            ServiceConfigurationContext context,
            string audience)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = audience;
                });
        }
    }
}

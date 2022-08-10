using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web.Helpers
{
    public static class WebApplicationBuilderExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(JwtBearerDefaults.AuthenticationScheme);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Audience = section[nameof(options.Audience)];
                    options.RequireHttpsMetadata = section.GetValue<bool>(nameof(options.RequireHttpsMetadata));
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(section["Password"]))
                    };
                });

            return services;
        }
    }
}

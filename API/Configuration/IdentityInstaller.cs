using System.Text;
using Domain;
using Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Persistence;

namespace API.Configuration
{
    public class IdentityInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection service, IConfiguration configuration)
        {
            service.AddIdentityCore<AppUser>(options=>
            {
                options.Password.RequireNonAlphanumeric=false;
                options.User.RequireUniqueEmail=true;
            })
            .AddEntityFrameworkStores<DataContext>();

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TockenKey"]));

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt=>
            {
                opt.TokenValidationParameters=new TokenValidationParameters
                {
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=key,
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });
            service.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsActivityHost", policy =>
                {
                    policy.Requirements.Add(new IsHostRequirement());
                });
            });
            service.AddTransient<IAuthorizationHandler, IsHostRequirementHandler>();
        }
    }
}
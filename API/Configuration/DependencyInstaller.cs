using API.Services;
using Application.Interfaces;
using Infrastructure.Security;

namespace API.Configuration
{
    public class DependencyInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<TockenService>();
            service.AddHttpContextAccessor();
            service.AddScoped<IUserAccessor,UserAccessor>();
        }
    }
}
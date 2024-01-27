using API.Services;

namespace API.Configuration
{
    public class DependencyInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection service, IConfiguration configuration)
        {
            service.AddScoped<TockenService>();
        }
    }
}
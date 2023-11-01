using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration
{
    public class DependencyInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection service, IConfiguration configuration)
        {
            //service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
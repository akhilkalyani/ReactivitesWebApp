using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Configuration
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection service,IConfiguration configuration);
    }
}
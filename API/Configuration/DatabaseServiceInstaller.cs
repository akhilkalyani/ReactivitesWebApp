using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Configuration
{
    public class DatabaseServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
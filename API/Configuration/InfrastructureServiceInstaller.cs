using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Activities;
using Application.Core;

namespace API.Configuration
{
    public class InfrastructureServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection service, IConfiguration configuration)
        {
            service.AddControllers();
            service.AddEndpointsApiExplorer();
            service.AddSwaggerGen();
            service.AddCors(option=>{
                option.AddPolicy("CorsPolicy",policy=>{
                    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                });
            });
            service.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
            service.AddAutoMapper(typeof(MappingProfiles).Assembly);
        }
    }
}
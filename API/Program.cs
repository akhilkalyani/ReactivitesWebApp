using API.Configuration;
using API.Middleware;
using Application.Activities;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence;

internal class Program
{
    private static  void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        
        builder.Services.InstallServices(builder.Configuration,typeof(IServiceInstaller).Assembly);
        
        var app = builder.Build();
        app.UseMiddleware<ExceptionMiddleware>();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors("CorsPolicy");
        //app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<DataContext>();
            context.Database.MigrateAsync();
            Seed.SeedData(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured during migration.");
        }

        app.Run();
    }
}
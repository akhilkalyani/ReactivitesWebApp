using System.Reflection;

namespace API.Configuration
{
    public static class ConfigurationController
    {
        public static IServiceCollection InstallServices(
            this IServiceCollection service,
            IConfiguration configuration,
            params Assembly[] assemblies
        )
        {
            var serviceInstallers=assemblies
            .SelectMany(a=>a.DefinedTypes)
            .Where(IsAssignableType<IServiceInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServiceInstaller>();

            
            foreach (var serviceInstaller in serviceInstallers)
            {
                serviceInstaller.Install(service,configuration);
            }
            return service;
        }
        static bool IsAssignableType<T>(TypeInfo type) =>
                typeof(T).IsAssignableFrom(type) &&
                !type.IsAbstract &&
                !type.IsInterface;
    }
}
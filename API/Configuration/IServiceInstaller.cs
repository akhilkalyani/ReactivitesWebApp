namespace API.Configuration
{
    public interface IServiceInstaller
    {
        void Install(IServiceCollection service,IConfiguration configuration);
    }
}
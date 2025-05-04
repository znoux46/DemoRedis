namespace DemoRedis.Installers
{
    public interface IInstaller
    {
        void InstallServices(IServiceCollection service, IConfiguration configuration);
    }
}

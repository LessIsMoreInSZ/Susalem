using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace SusalemAbp.Shared.Hosting
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpDataModule)
    )]
    public class SusalemAbpSharedHostingModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureDatabaseConnections();
        }
        private void ConfigureDatabaseConnections()
        {
            Configure<AbpDbConnectionOptions>(options =>
            {
             
                // 配置多数据库
                //options.Databases.Configure("IdentityService", database =>
                //{
                //    database.MappedConnections.Add("AbpIdentity");
                //    database.MappedConnections.Add("AbpIdentityServer");
                //});
            });
        }
    }
}
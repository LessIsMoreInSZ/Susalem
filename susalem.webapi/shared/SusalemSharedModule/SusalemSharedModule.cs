using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace SusalemSharedModule;

[DependsOn(typeof(AbpSwashbuckleModule))]
public class SusalemSharedModule : AbpModule { }
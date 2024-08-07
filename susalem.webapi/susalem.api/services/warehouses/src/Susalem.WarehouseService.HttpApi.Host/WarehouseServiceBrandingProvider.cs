using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Susalem.WarehouseService;

[Dependency(ReplaceServices = true)]
public class WarehouseServiceBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "WarehouseService";
}

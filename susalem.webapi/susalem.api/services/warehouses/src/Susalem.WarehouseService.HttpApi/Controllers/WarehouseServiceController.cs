using Susalem.WarehouseService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Susalem.WarehouseService.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WarehouseServiceController : AbpControllerBase
{
    protected WarehouseServiceController()
    {
        LocalizationResource = typeof(WarehouseServiceResource);
    }
}

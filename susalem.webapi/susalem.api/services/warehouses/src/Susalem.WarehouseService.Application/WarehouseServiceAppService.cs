using System;
using System.Collections.Generic;
using System.Text;
using Susalem.WarehouseService.Localization;
using Volo.Abp.Application.Services;

namespace Susalem.WarehouseService;

/* Inherit your application services from this class.
 */
public abstract class WarehouseServiceAppService : ApplicationService
{
    protected WarehouseServiceAppService()
    {
        LocalizationResource = typeof(WarehouseServiceResource);
    }
}

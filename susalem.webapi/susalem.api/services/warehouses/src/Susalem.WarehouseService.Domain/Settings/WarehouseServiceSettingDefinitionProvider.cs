using Volo.Abp.Settings;

namespace Susalem.WarehouseService.Settings;

public class WarehouseServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WarehouseServiceSettings.MySetting1));
    }
}

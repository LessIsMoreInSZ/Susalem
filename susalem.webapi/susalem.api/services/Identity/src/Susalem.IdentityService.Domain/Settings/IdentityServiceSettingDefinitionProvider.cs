using Volo.Abp.Settings;

namespace Susalem.IdentityService.Settings;

public class IdentityServiceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(IdentityServiceSettings.MySetting1));
    }
}

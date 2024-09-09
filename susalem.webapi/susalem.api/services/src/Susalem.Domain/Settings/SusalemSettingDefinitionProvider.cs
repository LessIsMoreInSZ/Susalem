using Volo.Abp.Settings;

namespace Susalem.Settings;

public class SusalemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(SusalemSettings.MySetting1));
    }
}

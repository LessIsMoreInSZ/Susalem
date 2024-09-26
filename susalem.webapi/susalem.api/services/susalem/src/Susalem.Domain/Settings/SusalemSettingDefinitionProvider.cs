﻿


using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Susalem.Settings;

public class SusalemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
      

    }
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityResource>(name);
    }
}

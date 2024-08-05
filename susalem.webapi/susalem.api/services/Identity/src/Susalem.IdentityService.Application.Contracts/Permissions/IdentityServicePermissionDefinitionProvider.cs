using Susalem.IdentityService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Susalem.IdentityService.Permissions;

public class IdentityServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(IdentityServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(IdentityServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<IdentityServiceResource>(name);
    }
}

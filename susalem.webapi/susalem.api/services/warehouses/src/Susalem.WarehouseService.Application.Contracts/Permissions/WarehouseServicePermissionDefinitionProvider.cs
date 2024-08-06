using Susalem.WarehouseService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Susalem.WarehouseService.Permissions;

public class WarehouseServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WarehouseServicePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(WarehouseServicePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WarehouseServiceResource>(name);
    }
}

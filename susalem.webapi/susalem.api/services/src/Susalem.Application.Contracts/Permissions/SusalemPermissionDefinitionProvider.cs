using Susalem.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Susalem.Permissions;

public class SusalemPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SusalemPermissions.GroupName);
        #region Identity

        var identityGroup = context.GetGroupOrNull(Volo.Abp.Identity.IdentityPermissions.GroupName);
        if (identityGroup != null)
        {
            var userPermission = identityGroup.GetPermissionOrNull(Volo.Abp.Identity.IdentityPermissions.Users.Default);
            if (userPermission != null)
            {
                userPermission.AddChild(SusalemPermissions.Users.ResetPassword, L("Permission:ResetPassword"));
                userPermission.AddChild(SusalemPermissions.Users.ManageClaims, L("Permission:ManageClaims"));
                userPermission.AddChild(SusalemPermissions.Users.ManageOrganizationUnits, L("Permission:ManageOrganizationUnits"));
            }

            var rolePermission = identityGroup.GetPermissionOrNull(Volo.Abp.Identity.IdentityPermissions.Roles.Default);
            if (rolePermission != null)
            {
                rolePermission.AddChild(SusalemPermissions.Roles.ManageClaims, L("Permission:ManageClaims"));
                rolePermission.AddChild(SusalemPermissions.Roles.ManageOrganizationUnits, L("Permission:ManageOrganizationUnits"));
            }

            var origanizationUnitPermission = identityGroup.AddPermission(SusalemPermissions.OrganizationUnits.Default, L("Permission:OrganizationUnitManagement"));
            origanizationUnitPermission.AddChild(SusalemPermissions.OrganizationUnits.Create, L("Permission:Create"));
            origanizationUnitPermission.AddChild(SusalemPermissions.OrganizationUnits.Update, L("Permission:Edit"));
            origanizationUnitPermission.AddChild(SusalemPermissions.OrganizationUnits.Delete, L("Permission:Delete"));
            origanizationUnitPermission.AddChild(SusalemPermissions.OrganizationUnits.ManageRoles, L("Permission:ManageRoles"));
            origanizationUnitPermission.AddChild(SusalemPermissions.OrganizationUnits.ManageUsers, L("Permission:ManageUsers"));
            origanizationUnitPermission.AddChild(SusalemPermissions.OrganizationUnits.ManagePermissions, L("Permission:ChangePermissions"));

            // 2020-10-23 修复Bug 租户用户也必须能查询自定义的声明, 管理权限只能为主机
            var identityClaimType = identityGroup.AddPermission(SusalemPermissions.IdentityClaimType.Default, L("Permission:IdentityClaimTypeManagement"));
            identityClaimType.AddChild(SusalemPermissions.IdentityClaimType.Create, L("Permission:Create"), MultiTenancySides.Host);
            identityClaimType.AddChild(SusalemPermissions.IdentityClaimType.Update, L("Permission:Edit"), MultiTenancySides.Host);
            identityClaimType.AddChild(SusalemPermissions.IdentityClaimType.Delete, L("Permission:Delete"), MultiTenancySides.Host);
            #endregion

        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SusalemResource>(name);
    }
}

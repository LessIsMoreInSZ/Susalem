using Microsoft.AspNetCore.Authorization;

using Susalem.Identity;
using Susalem.Identity.Dto;
using Susalem.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Susalem.Identity
{
    [Authorize(Volo.Abp.Identity.IdentityPermissions.Roles.Default)]
    public class IdentityRoleAppService : Volo.Abp.Identity.IdentityAppServiceBase, IIdentityRoleAppService
    {
        protected Identity.IIdentityRoleRepository IdentityRoleRepository { get; }
        protected Volo.Abp.Identity.OrganizationUnitManager OrganizationUnitManager { get; }
        public IdentityRoleAppService(
            Identity.IIdentityRoleRepository roleRepository,
           Volo.Abp.Identity.OrganizationUnitManager organizationUnitManager)
        {
            OrganizationUnitManager = organizationUnitManager;
            IdentityRoleRepository = roleRepository;
        }

        #region OrganizationUnit

        [Authorize(SusalemPermissions.Roles.ManageOrganizationUnits)]
        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetOrganizationUnitsAsync(Guid id)
        {
            var origanizationUnits = await IdentityRoleRepository.GetOrganizationUnitsAsync(id);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<Volo.Abp.Identity.OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits));
        }

        [Authorize(SusalemPermissions.Roles.ManageOrganizationUnits)]
        public async virtual Task SetOrganizationUnitsAsync(Guid id, IdentityRoleAddOrRemoveOrganizationUnitDto input)
        {
            var origanizationUnits = await IdentityRoleRepository.GetOrganizationUnitsAsync(id, true);

            var notInRoleOuIds = input.OrganizationUnitIds.Where(ouid => !origanizationUnits.Any(ou => ou.Id.Equals(ouid)));

            foreach (var ouId in notInRoleOuIds)
            {
                await OrganizationUnitManager.AddRoleToOrganizationUnitAsync(id, ouId);
            }

            var removeRoleOriganzationUnits = origanizationUnits.Where(ou => !input.OrganizationUnitIds.Contains(ou.Id));
            foreach (var origanzationUnit in removeRoleOriganzationUnits)
            {
                origanzationUnit.RemoveRole(id);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(SusalemPermissions.Roles.ManageOrganizationUnits)]
        public async virtual Task RemoveOrganizationUnitsAsync(Guid id, Guid ouId)
        {
            await OrganizationUnitManager.RemoveRoleFromOrganizationUnitAsync(id, ouId);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion

        #region ClaimType

        public async virtual Task<ListResultDto<IdentityClaimDto>> GetClaimsAsync(Guid id)
        {
            var role = await IdentityRoleRepository.GetAsync(id);

            return new ListResultDto<IdentityClaimDto>(ObjectMapper.Map<ICollection<Volo.Abp.Identity.IdentityRoleClaim>, List<IdentityClaimDto>>(role.Claims));
        }

        [Authorize(SusalemPermissions.Roles.ManageClaims)]
        public async virtual Task AddClaimAsync(Guid id, IdentityRoleClaimCreateDto input)
        {
            var role = await IdentityRoleRepository.GetAsync(id);
            var claim = new Claim(input.ClaimType, input.ClaimValue);
            if (role.FindClaim(claim) != null)
            {
                throw new UserFriendlyException(L["RoleClaimAlreadyExists"]);
            }

            role.AddClaim(GuidGenerator, claim);
            await IdentityRoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(SusalemPermissions.Roles.ManageClaims)]
        public async virtual Task UpdateClaimAsync(Guid id, IdentityRoleClaimUpdateDto input)
        {
            var role = await IdentityRoleRepository.GetAsync(id);
            var oldClaim = role.FindClaim(new Claim(input.ClaimType, input.ClaimValue));
            if (oldClaim != null)
            {
                role.RemoveClaim(oldClaim.ToClaim());
                role.AddClaim(GuidGenerator, new Claim(input.ClaimType, input.NewClaimValue));

                await IdentityRoleRepository.UpdateAsync(role);

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [Authorize(SusalemPermissions.Roles.ManageClaims)]
        public async virtual Task DeleteClaimAsync(Guid id, IdentityRoleClaimDeleteDto input)
        {
            var role = await IdentityRoleRepository.GetAsync(id);
            role.RemoveClaim(new Claim(input.ClaimType, input.ClaimValue));

            await IdentityRoleRepository.UpdateAsync(role);

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        #endregion
    }
}

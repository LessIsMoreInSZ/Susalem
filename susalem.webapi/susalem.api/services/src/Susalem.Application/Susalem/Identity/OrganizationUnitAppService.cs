using Microsoft.AspNetCore.Authorization;

using Susalem.Identity;
using Susalem.Identity.Dto;
using Susalem.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace Susalem.Identity
{
    [Authorize(SusalemPermissions.OrganizationUnits.Default)]
    public class OrganizationUnitAppService : Volo.Abp.Identity.IdentityAppServiceBase, IOrganizationUnitAppService
    {
        protected Volo.Abp.Identity.OrganizationUnitManager OrganizationUnitManager { get; }
        protected Volo.Abp.Identity.IOrganizationUnitRepository OrganizationUnitRepository { get; }

        protected Volo.Abp.Identity.IdentityUserManager UserManager { get; }
        protected IIdentityRoleRepository RoleRepository { get; }
        protected IIdentityUserRepository UserRepository { get; }

        public OrganizationUnitAppService(
            Volo.Abp.Identity.IdentityUserManager userManager,
            IIdentityRoleRepository roleRepository,
            IIdentityUserRepository userRepository,
           Volo.Abp.Identity.OrganizationUnitManager organizationUnitManager,
            Volo.Abp.Identity.IOrganizationUnitRepository organizationUnitRepository)
        {
            UserManager = userManager;
            RoleRepository = roleRepository;
            UserRepository = userRepository;
            OrganizationUnitManager = organizationUnitManager;
            OrganizationUnitRepository = organizationUnitRepository;
        }

        [Authorize(SusalemPermissions.OrganizationUnits.Create)]
        public async virtual Task<OrganizationUnitDto> CreateAsync(OrganizationUnitCreateDto input)
        {
            var origanizationUnit = new Volo.Abp.Identity.OrganizationUnit(
                GuidGenerator.Create(), input.DisplayName, input.ParentId, CurrentTenant.Id)
            {
                CreationTime = Clock.Now
            };
            input.MapExtraPropertiesTo(origanizationUnit);

            await OrganizationUnitManager.CreateAsync(origanizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(origanizationUnit);
        }

        [Authorize(SusalemPermissions.OrganizationUnits.Delete)]
        public async virtual Task DeleteAsync(Guid id)
        {
            var origanizationUnit = await OrganizationUnitRepository.FindAsync(id);
            if (origanizationUnit == null)
            {
                return;
            }
            await OrganizationUnitManager.DeleteAsync(id);
        }

        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetRootAsync()
        {
            var rootOriganizationUnits = await OrganizationUnitManager.FindChildrenAsync(null, recursive: false);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<Volo.Abp.Identity.OrganizationUnit>, List<OrganizationUnitDto>>(rootOriganizationUnits));
        }

        public async virtual Task<ListResultDto<OrganizationUnitDto>> FindChildrenAsync(OrganizationUnitGetChildrenDto input)
        {
            var origanizationUnitChildren = await OrganizationUnitManager.FindChildrenAsync(input.Id, input.Recursive);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<Volo.Abp.Identity.OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnitChildren));
        }

        public async virtual Task<OrganizationUnitDto> GetAsync(Guid id)
        {
            var origanizationUnit = await OrganizationUnitRepository.FindAsync(id);

            return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(origanizationUnit);
        }

        public async virtual Task<OrganizationUnitDto> GetLastChildOrNullAsync(Guid? parentId)
        {
            var origanizationUnitLastChildren = await OrganizationUnitManager.GetLastChildOrNullAsync(parentId);

            return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(origanizationUnitLastChildren);
        }

        public async virtual Task<ListResultDto<OrganizationUnitDto>> GetAllListAsync()
        {
            var origanizationUnits = await OrganizationUnitRepository.GetListAsync(false);

            return new ListResultDto<OrganizationUnitDto>(
                ObjectMapper.Map<List<Volo.Abp.Identity.OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits));
        }

        public async virtual Task<PagedResultDto<OrganizationUnitDto>> GetListAsync(OrganizationUnitGetByPagedDto input)
        {
            var origanizationUnitCount = await OrganizationUnitRepository.GetCountAsync();
            var origanizationUnits = await OrganizationUnitRepository
                .GetListAsync(input.Sorting, input.MaxResultCount, input.SkipCount, false);

            return new PagedResultDto<OrganizationUnitDto>(origanizationUnitCount,
                ObjectMapper.Map<List<Volo.Abp.Identity.OrganizationUnit>, List<OrganizationUnitDto>>(origanizationUnits));
        }

        [Authorize(SusalemPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<ListResultDto<string>> GetRoleNamesAsync(Guid id)
        {
            var inOrignizationUnitRoleNames = await UserRepository.GetRoleNamesInOrganizationUnitAsync(id);
            return new ListResultDto<string>(inOrignizationUnitRoleNames);
        }

        [Authorize(SusalemPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetUnaddedRolesAsync(Guid id, OrganizationUnitGetUnaddedRoleByPagedDto input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var origanizationUnitRoleCount = await OrganizationUnitRepository
                .GetUnaddedRolesCountAsync(origanizationUnit, input.Filter);

            var origanizationUnitRoles = await OrganizationUnitRepository
                .GetUnaddedRolesAsync(origanizationUnit,
                input.Sorting, input.MaxResultCount,
                input.SkipCount, input.Filter);

            return new PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>(origanizationUnitRoleCount,
                ObjectMapper.Map<List<Volo.Abp.Identity.IdentityRole>, List<Volo.Abp.Identity.IdentityRoleDto>>(origanizationUnitRoles));
        }

        [Authorize(SusalemPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task<PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>> GetRolesAsync(Guid id, PagedAndSortedResultRequestDto input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var origanizationUnitRoleCount = await OrganizationUnitRepository
                .GetRolesCountAsync(origanizationUnit);

            var origanizationUnitRoles = await OrganizationUnitRepository
                .GetRolesAsync(origanizationUnit,
                input.Sorting, input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<Volo.Abp.Identity.IdentityRoleDto>(origanizationUnitRoleCount,
                ObjectMapper.Map<List<Volo.Abp.Identity.IdentityRole>, List<Volo.Abp.Identity.IdentityRoleDto>>(origanizationUnitRoles));
        }


        [Authorize(SusalemPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task<PagedResultDto<Volo.Abp.Identity.IdentityUserDto>> GetUnaddedUsersAsync(Guid id, OrganizationUnitGetUnaddedUserByPagedDto input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var origanizationUnitUserCount = await OrganizationUnitRepository
                .GetUnaddedUsersCountAsync(origanizationUnit, input.Filter);
            var origanizationUnitUsers = await OrganizationUnitRepository
                .GetUnaddedUsersAsync(origanizationUnit,
                input.Sorting, input.MaxResultCount,
                input.SkipCount, input.Filter);

            return new PagedResultDto<Volo.Abp.Identity.IdentityUserDto>(origanizationUnitUserCount,
                ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<Volo.Abp.Identity.IdentityUserDto>>(origanizationUnitUsers));
        }

        [Authorize(SusalemPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task<PagedResultDto<Volo.Abp.Identity.IdentityUserDto>> GetUsersAsync(Guid id, Volo.Abp.Identity.GetIdentityUsersInput input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var origanizationUnitUserCount = await OrganizationUnitRepository
                .GetMembersCountAsync(origanizationUnit, input.Filter);
            var origanizationUnitUsers = await OrganizationUnitRepository
                .GetMembersAsync(origanizationUnit,
                input.Sorting, input.MaxResultCount,
                input.SkipCount, input.Filter);

            return new PagedResultDto<Volo.Abp.Identity.IdentityUserDto>(origanizationUnitUserCount,
                ObjectMapper.Map<List<Volo.Abp.Identity.IdentityUser>, List<Volo.Abp.Identity.IdentityUserDto>>(origanizationUnitUsers));
        }

        [Authorize(SusalemPermissions.OrganizationUnits.Update)]
        public async virtual Task MoveAsync(Guid id, OrganizationUnitMoveDto input)
        {
            await OrganizationUnitManager.MoveAsync(id, input.ParentId);
        }

        [Authorize(SusalemPermissions.OrganizationUnits.Update)]
        public async virtual Task<OrganizationUnitDto> UpdateAsync(Guid id, OrganizationUnitUpdateDto input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);
            origanizationUnit.DisplayName = input.DisplayName;
            input.MapExtraPropertiesTo(origanizationUnit);

            await OrganizationUnitManager.UpdateAsync(origanizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Volo.Abp.Identity.OrganizationUnit, OrganizationUnitDto>(origanizationUnit);
        }

        [Authorize(SusalemPermissions.OrganizationUnits.ManageUsers)]
        public async virtual Task AddUsersAsync(Guid id, OrganizationUnitAddUserDto input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);
            var users = await UserRepository.GetListByIdListAsync(input.UserIds, includeDetails: true);

            // 调用内部方法设置用户组织机构
            foreach (var user in users)
            {
                await UserManager.AddToOrganizationUnitAsync(user, origanizationUnit);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [Authorize(SusalemPermissions.OrganizationUnits.ManageRoles)]
        public async virtual Task AddRolesAsync(Guid id, OrganizationUnitAddRoleDto input)
        {
            var origanizationUnit = await OrganizationUnitRepository.GetAsync(id);

            var roles = await RoleRepository.GetListByIdListAsync(input.RoleIds, includeDetails: true);

            foreach (var role in roles)
            {
                await OrganizationUnitManager.AddRoleToOrganizationUnitAsync(role, origanizationUnit);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}

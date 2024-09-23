using AutoMapper;
using Susalem.Identity.Dto;
using Volo.Abp.Identity;

namespace Susalem;

public class SusalemApplicationAutoMapperProfile : Profile
{
    public SusalemApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<IdentityClaimType, IdentityClaimTypeDto>()
                       .MapExtraProperties();
        CreateMap<IdentityUserClaim, IdentityClaimDto>();
        CreateMap<IdentityRoleClaim, IdentityClaimDto>();

        CreateMap<IdentityUser, IdentityUserDto>()
            .MapExtraProperties();

        CreateMap<IdentityRole, IdentityRoleDto>()
            .MapExtraProperties();

        CreateMap<OrganizationUnit, OrganizationUnitDto>()
            .MapExtraProperties();
    }
}

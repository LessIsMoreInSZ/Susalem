using AutoMapper;

using Susalem.Abp.Identity;

using Volo.Abp.Identity;
namespace Susalem;

public class SusalemApplicationAutoMapperProfile : Profile
{
    public SusalemApplicationAutoMapperProfile()
    {
        CreateMap<Volo.Abp.Identity.IdentityClaimType, IdentityClaimTypeDto>()
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

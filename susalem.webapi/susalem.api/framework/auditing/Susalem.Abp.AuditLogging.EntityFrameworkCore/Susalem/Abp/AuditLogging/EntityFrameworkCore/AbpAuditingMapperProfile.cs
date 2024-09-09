using AutoMapper;

namespace Susalem.Abp.AuditLogging.EntityFrameworkCore;

public class AbpAuditingMapperProfile : Profile
{
    public AbpAuditingMapperProfile()
    {
        CreateMap<Volo.Abp.AuditLogging.AuditLogAction, Susalem.Abp.AuditLogging.AuditLogAction>()
            .MapExtraProperties();
        CreateMap<Volo.Abp.AuditLogging.EntityPropertyChange, Susalem.Abp.AuditLogging.EntityPropertyChange>();
        CreateMap<Volo.Abp.AuditLogging.EntityChange, Susalem.Abp.AuditLogging.EntityChange>()
            .MapExtraProperties();
        CreateMap<Volo.Abp.AuditLogging.AuditLog, Susalem.Abp.AuditLogging.AuditLog>()
            .MapExtraProperties();

        CreateMap<Volo.Abp.Identity.IdentitySecurityLog, Susalem.Abp.AuditLogging.SecurityLog>()
            .MapExtraProperties();
    }
}

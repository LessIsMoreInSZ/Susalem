using AutoMapper;
using Susalem.Abp.Auditing.AuditLogs;
using Susalem.Abp.Auditing.Logging;
using Susalem.Abp.Auditing.SecurityLogs;
using Susalem.Abp.AuditLogging;
using Susalem.Abp.Logging;

namespace Susalem.Abp.Auditing;

public class AbpAuditingMapperProfile : Profile
{
    public AbpAuditingMapperProfile()
    {
        CreateMap<AuditLogAction, AuditLogActionDto>()
            .MapExtraProperties();
        CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();
        CreateMap<EntityChangeWithUsername, EntityChangeWithUsernameDto>();
        CreateMap<EntityChange, EntityChangeDto>()
            .MapExtraProperties();
        CreateMap<AuditLog, AuditLogDto>()
            .MapExtraProperties();

        CreateMap<SecurityLog, SecurityLogDto>()
            .MapExtraProperties();

        CreateMap<LogField, LogFieldDto>();
        CreateMap<LogException, LogExceptionDto>();
        CreateMap<LogInfo, LogDto>();
    }
}

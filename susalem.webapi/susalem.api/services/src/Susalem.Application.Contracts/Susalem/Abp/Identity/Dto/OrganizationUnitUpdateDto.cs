using Volo.Abp.ObjectExtending;

namespace Susalem.Abp.Identity
{
    public class OrganizationUnitUpdateDto : ExtensibleObject
    {
        public string DisplayName { get; set; }
    }
}

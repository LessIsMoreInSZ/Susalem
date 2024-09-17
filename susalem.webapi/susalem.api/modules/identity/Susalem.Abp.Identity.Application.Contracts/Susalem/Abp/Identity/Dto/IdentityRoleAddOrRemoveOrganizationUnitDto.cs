using System;
using System.ComponentModel.DataAnnotations;

namespace Susalem.Abp.Identity
{
    public class IdentityRoleAddOrRemoveOrganizationUnitDto
    {
        [Required]
        public Guid[] OrganizationUnitIds { get; set; }
    }
}

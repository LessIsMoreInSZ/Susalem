﻿using Volo.Abp.Application.Dtos;

namespace Susalem.Abp.Identity
{
    public class OrganizationUnitGetUnaddedUserByPagedDto : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

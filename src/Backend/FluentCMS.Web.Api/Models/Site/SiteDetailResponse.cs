﻿namespace FluentCMS.Web.Api.Models;

public class SiteDetailResponse : BaseSiteAssociatedResponse
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public List<string> Urls { get; set; } = [];
    public Guid LayoutId { get; set; }
    public Guid DetailLayoutId { get; set; }
    public Guid EditLayoutId { get; set; }
    public List<RoleDetailResponse> AdminRoles { get; set; } = [];
    public List<RoleDetailResponse> ContributorRoles { get; set; } = [];
    public List<RoleDetailResponse> AllRoles { get; set; } = [];
}

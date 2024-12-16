using System.Security.Claims;
using MG.BuildingBlock.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace MG.BuildingBlock.Api.Configs;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : ICurrentUser
{
    public string IPAddress => httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
}

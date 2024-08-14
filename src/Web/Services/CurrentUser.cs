using System.Security.Claims;
using HireHub.Api.Application.Common.Interfaces;

namespace HireHub.Api.Web.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
}

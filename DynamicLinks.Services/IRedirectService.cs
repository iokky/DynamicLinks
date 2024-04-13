using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public interface IRedirectService
    {
        public ILinkResponse GetLinkResponse(ILinkRequest getLinkRequest);
    }
}

using DynamicLinks.Domain.Requests;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public interface IManagedService
    {
        public ILinkResponse GetLinkResponse(GetLinkRequest getLinkRequest);
    }
}

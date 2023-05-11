using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public interface ILinkReponseFactory
    {
        public ILinkResponse CreateWebLink(ILinkRequest link);
        public ILinkResponse CreateAndroidLink(ILinkRequest link);
        public ILinkResponse CreateIOSLink(ILinkRequest link);
    }
}

using DynamicLinks.Domain.Entity;
using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public interface ILinkReponseFactory
    {
        public ILinkResponse GetWebLink(ILinkRequest link);
        public ILinkResponse GetAndroidLink(ILinkRequest link);
        public ILinkResponse GetIOSLink(ILinkRequest link);

        public void CreateLink(DynamicLinkEntity link);
    }
}

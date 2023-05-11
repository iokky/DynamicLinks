using DynamicLinks.Domain.Enums;
using DynamicLinks.Domain.Requests;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public class RedirectHandlerService : IManagedService
    {
        private readonly ILinkReponseFactory _responseFactory;

        public RedirectHandlerService(ILinkReponseFactory reponseFactory)
        {
            _responseFactory = reponseFactory;
        }

        public ILinkResponse GetLinkResponse(GetLinkRequest getLinkRequest)
        {
            //TODO сделать ENUM для платформ и OS и переписать на switch-case
            if (getLinkRequest.Mobile)
            {
                if (getLinkRequest.Platform.ToString().Equals(OS.Android.ToString()))
                {
                    return _responseFactory.CreateAndroidLink(getLinkRequest);
                }
                else
                {
                    return _responseFactory.CreateIOSLink(getLinkRequest);
                }
            }
            return _responseFactory.CreateWebLink(getLinkRequest);
        }
    }
}

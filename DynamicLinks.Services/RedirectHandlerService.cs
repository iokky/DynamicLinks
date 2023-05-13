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
            //TODO обработка роботных запросов или PostMan
            if (getLinkRequest.Mobile)
            {
                if (Enum.TryParse(getLinkRequest.Platform, out OS os))
                {
                    switch (os)
                    {
                        case OS.Android:
                            return _responseFactory.CreateAndroidLink(getLinkRequest);
                        case OS.iOS:
                            return _responseFactory.CreateIOSLink(getLinkRequest);
                        default:
                            return _responseFactory.CreateWebLink(getLinkRequest);
                    }
                }
            }
            return _responseFactory.CreateWebLink(getLinkRequest);
        }
    }
}

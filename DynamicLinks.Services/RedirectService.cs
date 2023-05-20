using DynamicLinks.Domain.Enums;
using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public class RedirectService : IRedirectService
    {
        private readonly ILinkReponseFactory _responseFactory;

        public RedirectService(ILinkReponseFactory reponseFactory)
        {
            _responseFactory = reponseFactory;
        }

        //TODO Продумать защиту от програмных запросов 
        //у запроса с помощбю PostMan параметр Platform == "Unknown Platform"
        public ILinkResponse GetLinkResponse(ILinkRequest getLinkRequest)
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

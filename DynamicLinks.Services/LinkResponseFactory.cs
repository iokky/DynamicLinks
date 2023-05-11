using DynamicLinks.Dal.Repositories.Interfaces;
using DynamicLinks.Domain.Entity;
using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response;
using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Services
{
    public class LinkResponseFactory : ILinkReponseFactory
    {
        private readonly IRepository<DynamicLinkEntity> _repository;

        public LinkResponseFactory(IRepository<DynamicLinkEntity> repository)
        {
            _repository = repository;
        }
        public ILinkResponse CreateAndroidLink(ILinkRequest link)
        {
            var androidLink = _repository.GetAsync(link.ShortLink).Result;
            if (androidLink is not null)
            {
                var check = androidLink.AndroidLink is null;
                if (androidLink.AndroidLink is null)
                {
                    //TODO обработка Null в androidLink
                    return new AndroidLinkResponse(androidLink.AndroidDefaultLink.ToString());
                }
                //TODO обработка Null в androidLink
                
                return new AndroidLinkResponse(androidLink.AndroidLink.ToString());
            }
            return new AndroidLinkResponse("/");
        }


        public ILinkResponse CreateIOSLink(ILinkRequest link)
        {
            var iosLink = _repository.GetAsync(link.ShortLink).Result;
            if (iosLink is not null)
            {

                if (iosLink == default)
                {
                    //TODO обработка Null в iosLink
                    return new IOSLinkResponse(_repository.GetAsync(link.ShortLink).Result.AndroidDefaultLink.ToString());
                }
                //TODO обработка Null в iosLink
                return new IOSLinkResponse(iosLink.ToString());
            }
            return new IOSLinkResponse("/");
        }


        public ILinkResponse CreateWebLink(ILinkRequest link)
        {
            var webLink = _repository.GetAsync(link.ShortLink).Result;
            //TODO обработка Null в webLink
            if (webLink is not null)
            {
                return new WebLinkResponse(webLink.WebLink!.ToString());
            }
            return new WebLinkResponse("/");
        }


    }
}

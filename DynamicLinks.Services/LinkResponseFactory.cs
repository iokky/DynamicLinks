using DynamicLinks.Dal.Repositories.Interfaces;
using DynamicLinks.Domain.Entity;
using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response;
using DynamicLinks.Domain.Response.Interfaces;
using System.Diagnostics;

namespace DynamicLinks.Services
{
    //TODO все мметоды в async
    public class LinkResponseFactory : ILinkReponseFactory
    {
        private readonly IRepository<DynamicLinkEntity> _repository;

        //TODO обработать сценарий падения redis
        private readonly ICacheService<DynamicLinkEntity> _redis;

        public LinkResponseFactory(
            IRepository<DynamicLinkEntity> repository,
            ICacheService<DynamicLinkEntity> redis)
        {
            _repository = repository;
            _redis = redis;
        }

        //ToDO прикрутить redis
        public ILinkResponse CreateAndroidLink(ILinkRequest link)
        {
            if (_redis.GetRedisStatusOk())
            {
                var redisData = _redis.Get(link.ShortLink).Result;
                if (redisData != null)
                {
                    if (redisData.AndroidLink is null)
                    {
                        //TODO Обработай null на AndroidDefaultLink
                        return new AndroidLinkResponse(redisData.AndroidDefaultLink.ToString());
                    }

                    return new AndroidLinkResponse(redisData.AndroidLink.ToString());
                }
            }


            var androidLink = _repository.GetAsync(link.ShortLink).Result;
            if (androidLink is not null)
            {
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

        //ToDO прикрутить redis
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
            if (_redis.GetRedisStatusOk())
            {
                var redisData = _redis.Get(link.ShortLink).Result;
                if (redisData is not null)
                {
                    Debug.WriteLine($"data from redis {redisData.ShortLink} - {redisData.WebLink}");
                    return new WebLinkResponse(redisData.WebLink!.ToString());
                }
            }
            else
            {
                var webLink = _repository.GetAsync(link.ShortLink).Result;
                //TODO обработка Null в webLink
                if (webLink is not null)
                {
                    //TODO Если Redis упал то тут будет задержка
                    //реализуй или проверку или переделай 
                    _redis.Set(webLink.ShortLink, webLink);
                    return new WebLinkResponse(webLink.WebLink!.ToString());
                }
            }

            return new WebLinkResponse("/");
        }

    }
}

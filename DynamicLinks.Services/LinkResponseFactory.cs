using DynamicLinks.Domain.Entity;
using DynamicLinks.Domain.Requests.Interfaces;
using DynamicLinks.Domain.Response;
using DynamicLinks.Domain.Response.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Text.Json;

namespace DynamicLinks.Services
{
    //TODO все методы в async
    public class LinkResponseFactory : ILinkReponseFactory
    {
        //TODO обработать сценарий падения redis
        private readonly IDistributedCache _redis;
        private readonly Stopwatch sw = Stopwatch.StartNew();

        public LinkResponseFactory(IDistributedCache redis)
        {
            _redis = redis;
        }
        #region CreateAndroidLink
        public ILinkResponse GetAndroidLink(ILinkRequest link)
        {
            sw.Start();
            var redisData = _redis.GetStringAsync(link.ShortLink.ToString()).Result;
            if (redisData is not null)
            {
                var resultAndroid = JsonSerializer.Deserialize<DynamicLinkEntity>(redisData);
                if (resultAndroid!.AndroidLink is null)
                {
                    sw.Stop();
                    Debug.WriteLine($"{sw.Elapsed.Seconds}s:{sw.Elapsed.Milliseconds}ms:{sw.Elapsed.Nanoseconds}ns");
                    return new AndroidLinkResponse(resultAndroid!.AndroidDefaultLink!.ToString());
                }
                sw.Stop();
                //TODO Обработай пустую дефолтную ссылку
                Debug.WriteLine($"{sw.Elapsed.Seconds}s:{sw.Elapsed.Milliseconds}ms:{sw.Elapsed.Nanoseconds}ns");
                return new AndroidLinkResponse(resultAndroid.AndroidLink.ToString());
            }
            sw.Stop();
            var time = sw.Elapsed;
            Debug.WriteLine($"{time.Seconds}s:{time.Milliseconds}ms:{time.Nanoseconds}ns");
            return new AndroidLinkResponse("/");
        }
        #endregion

        #region CreateIOSLink
        public ILinkResponse GetIOSLink(ILinkRequest link)
        {

            var redisData = _redis.GetStringAsync(link.ShortLink.ToString()).Result;
            if (redisData is not null)
            {
                var resultIOSLink = JsonSerializer.Deserialize<DynamicLinkEntity>(redisData);

                if (resultIOSLink!.IOSLink is not null)
                {
                    return new IOSLinkResponse(resultIOSLink.IOSLink.ToString());
                }
                //TODO Обработай пустую дефолтную ссылку
                return new IOSLinkResponse(resultIOSLink!.IOSDefaultLink!.ToString());
            }
            return new IOSLinkResponse("/");
        }
        #endregion

        #region CreateWebLink
        public ILinkResponse GetWebLink(ILinkRequest link)
        {
            sw.Start();
            var redisData = _redis.GetStringAsync(link.ShortLink.ToString()).Result;

            if (redisData is not null)
            {
                var resultWebLink = JsonSerializer.Deserialize<DynamicLinkEntity>(redisData);
                sw.Stop();
                Debug.WriteLine($"{sw.Elapsed.Seconds}s:{sw.Elapsed.Milliseconds}ms:{sw.Elapsed.Nanoseconds}ns");
                return new WebLinkResponse(resultWebLink!.WebLink!.ToString());
            }

            return new WebLinkResponse("/");
        }

        #endregion

        public void CreateLink(DynamicLinkEntity link)
        {
            var redisData = _redis.GetStringAsync(link.ShortLink.ToString()).Result;
            if (redisData is null)
            {
                //_redis.SetString(link.ShortLink.ToString(), link.ToString());
                _redis.Set(
                    link.ShortLink.ToString(),
                       JsonSerializer.SerializeToUtf8Bytes(link)
                );
            }
        }
    }
}

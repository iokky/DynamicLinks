using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Domain.Response
{
    public class IOSLinkResponse : ILinkResponse
    {
        public string Url { get; }
        public IOSLinkResponse(string url)
        {
            Url = url;
        }
    }
}

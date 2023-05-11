using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Domain.Response
{
    public class WebLinkResponse : ILinkResponse
    {
        public string Url { get ;  }
        public WebLinkResponse(string url)
        {
            Url = url;
        }
    }
}

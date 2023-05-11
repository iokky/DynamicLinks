using DynamicLinks.Domain.Response.Interfaces;

namespace DynamicLinks.Domain.Response
{
    public class AndroidLinkResponse: ILinkResponse
    {
        public string Url { get; }
        public AndroidLinkResponse(string url)
        {
            Url = url;
        }
    }
}

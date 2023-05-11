using DynamicLinks.Domain.Requests.Interfaces;

namespace DynamicLinks.Domain.Requests
{
    public class GetLinkRequest: ILinkRequest
    {
        public bool Mobile { get; set; }
        public string Platform { get; set; } = null!;
        public string ShortLink { get; set; } = null!;
    }
}

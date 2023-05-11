namespace DynamicLinks.Domain.Requests.Interfaces
{
    public interface ILinkRequest
    {
        public bool Mobile { get; set; }
        public string Platform { get; set; }
        public string ShortLink { get; set; }
    }
}

namespace DynamicLinks.Domain.Entity
{
    public class DynamicLinkEntity : BaseEntity
    {
        public string ShortLink { get; set; } = null!;
        public Uri? WebLink { get; set; }
        public Uri? AndroidLink { get; set; }
        public Uri? AndroidDefaultLink { get; set; }
        public Uri? IOSLink { get; set; }
        public Uri? IOSDefaultLink { get; set; }


        public bool Enable { get; set; }
    }
}
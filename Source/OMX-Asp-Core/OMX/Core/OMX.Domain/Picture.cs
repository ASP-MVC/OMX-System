namespace OMX.Domain
{
    public class Picture : BaseEntity<int>
    {
        public string Url { get; set; }

        public byte[] Content { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        #nullable enable
        public int? AdId { get; set; }

        public Ad? Ad { get; set; }
    }
}

namespace OMX.Domain
{
    using System.Collections.Generic;

    public class SubCategory : BaseEntity<int>
    {
        public string Title { get; set; }

        public int Visit { get; set; }

        public int CategoryId { get; set; }

        #nullable enable
        public Category? Category { get; set; }

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}

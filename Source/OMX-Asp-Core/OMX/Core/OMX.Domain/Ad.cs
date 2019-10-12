namespace OMX.Domain
{
    using System.Collections.Generic;

    public class Ad : BaseEntity<int>
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Price { get; set; }

        public int Visit { get; set; }

        public string OwnerId { get; set; }

        public User Owner { get; set; }

        public int SubCategoryId { get; set; }

        #nullable disable
        public SubCategory SubCategory { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<Picture> Pictures { get; set; } = new List<Picture>();
    }
}

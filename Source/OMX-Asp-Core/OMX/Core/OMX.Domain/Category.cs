namespace OMX.Domain
{
    using System.Collections.Generic;

    public class Category : BaseEntity<int>
    {
        public string Title { get; set; }

        public int Visit { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}

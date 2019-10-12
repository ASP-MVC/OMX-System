namespace OMX.Domain
{
    using System;

    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }
        
        #nullable enable
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
        
        #nullable enable
        public DateTime? DeletedOn { get; set; }
    }
}

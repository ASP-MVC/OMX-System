namespace OMX.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public string PictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        // TODO add index
        public bool IsDeleted { get; set; }

        #nullable enable
        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Ad> PublishedAds { get; set; } = new List<Ad>();

        public virtual ICollection<Picture> UploadedPictures { get; set; } = new List<Picture>();

        public virtual ICollection<Comment> RecievedComments { get; set; } = new List<Comment>();
    }
}

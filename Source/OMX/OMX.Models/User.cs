namespace OMX.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using OMX.Contracts;
    using OMX.Contracts.Models;

    public class User : IdentityUser, IEntity, IAuditInfo, IDeletableEntity
    {
        private ICollection<Ad> publishedAds;

        private ICollection<Picture> uploadedPictures;

        private ICollection<Comment> sendComments;

        private ICollection<Comment> recievedComments;

        public User()
        {
            // This will prevent UserManager of causing exception when registering new user
            this.CreatedOn = DateTime.Now;
            this.publishedAds = new HashSet<Ad>();
            this.uploadedPictures = new HashSet<Picture>();
            this.sendComments = new HashSet<Comment>();
            this.recievedComments = new HashSet<Comment>();
        }
        
        public string PictureUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Ad> PublishedAds
        {
            get
            {
                return this.publishedAds;
            }
            set
            {
                this.publishedAds = value;
            }
        }

        public virtual ICollection<Picture> UploadedPictures
        {
            get
            {
                return this.uploadedPictures;
            }
            set
            {
                this.uploadedPictures = value;
            }
        }

        public virtual ICollection<Comment> RecievedComments
        {
            get
            {
                return this.recievedComments;
            }

            set
            {
                this.recievedComments = value;
            }
        }

        public virtual ICollection<Comment> SendComments
        {
            get
            {
                return this.sendComments;
            }
            set
            {
                this.sendComments = value;
            }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
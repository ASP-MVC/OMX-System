namespace OMX.Application.Ads.Queries.GetSubCategoryAds
{
    using OMX.Domain;
    using System;
    using System.Linq.Expressions;

    public class AdPictureModel
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public byte[] Content { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public static Expression<Func<Picture, AdPictureModel>> Projection
           => p => new AdPictureModel
           {
               Id = p.Id,
               Url = p.Url,
               Content = p.Content,
               IsDeleted = p.IsDeleted,
               DeletedOn = p.DeletedOn
           };
    }
}

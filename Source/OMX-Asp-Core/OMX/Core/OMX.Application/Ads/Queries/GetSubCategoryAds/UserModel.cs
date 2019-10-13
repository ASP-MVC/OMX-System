namespace OMX.Application.Ads.Queries.GetSubCategoryAds
{
    using OMX.Domain;
    using System;
    using System.Linq.Expressions;

    public class UserModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string PictureUrl { get; set; }

        public string Email { get; set; }

        public int PublishedAdsCount { get; set; }

        public static Expression<Func<User, UserModel>> Projection
           => p => new UserModel
           {
               Id = p.Id,
               UserName = p.UserName,
               PictureUrl = p.PictureUrl,
               Email = p.Email,
               PublishedAdsCount = p.PublishedAds.Count
           };
    }
}

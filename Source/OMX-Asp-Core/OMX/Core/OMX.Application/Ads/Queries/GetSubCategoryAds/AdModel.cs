namespace OMX.Application.Ads.Queries.GetSubCategoryAds
{
    using OMX.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class AdModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Visit { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public UserModel Owner { get; set; }

        public string CategoryName { get; set; }

        public IEnumerable<AdPictureModel> Pictures { get; set; }

        public static Expression<Func<Ad, AdModel>> Projection
           => p => new AdModel
           {
               Id = p.Id,
               Title = p.Title,
               Content = p.Content,
               Price = p.Price,
               CreatedOn = p.CreatedOn,
               Visit = p.Visit,
               IsDeleted = p.IsDeleted,
               DeletedOn = p.DeletedOn,
               Owner = UserModel.Projection.Compile()(p.Owner),
               CategoryName = p.SubCategory.Category.Title,
               Pictures = p.Pictures.AsQueryable().Select(AdPictureModel.Projection).ToList()
           };
    }
}

using System.Runtime.Caching;
using System.Web;
using System.Web.Caching;
using OMX.Models;

namespace OMX.Infrastructure.Populators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using OMX.Data.UoW;
    using OMX.Infrastructure.Caching;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private IOMXData data;
        private ICacheService cache;

        public DropDownListPopulator(IOMXData data, ICacheService cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IEnumerable<Ad> GetUserAds(string id)
        {
            var ads = this.cache.Get<IEnumerable<Ad>>("userAds",
                () =>
                {
                    return this.data.Ads
                       .All()
                       .OrderByDescending(x => x.Visit)
                       .Where(u => u.OwnerId == id)
                       .ToList();
                });

            return ads;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            var categories = this.cache.Get<IEnumerable<Category>>("categoriesWithSubCategories",
                () =>
                {
                    return this.data.Categories
                       .All()
                       .OrderByDescending(x => x.Visit)
                       .ToList();
                });

            return categories;
        }

        public IEnumerable<Category> GetCategoriesWithSubCategories()
        {
            var categories = this.cache.Get<IEnumerable<Category>>("categoriesWithSubCategories",
                () =>
                {
                    return this.data.Categories
                       .All()
                       .OrderByDescending(x => x.Visit)
                       .Where(c => c.SubCategories.Any())
                       .ToList();
                }); 
            return categories;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            var categories = this.cache.Get<IEnumerable<SelectListItem>>("categories",
                () =>
                {
                    return this.data.Categories
                       .All()
                       .OrderBy(x => x.Title)
                       .Select(c => new SelectListItem()
                       {
                           Value = c.Id.ToString(),
                           Text = c.Title
                       }).ToList();
                });

            return categories;
        }

        public IEnumerable<SelectListItem> GetSubCategories()
        {
            var subCategories = this.cache.Get<IEnumerable<SelectListItem>>("subcategories",
                () =>
                {
                    return this.data.SubCategories
                       .All()
                       .OrderBy(x => x.Title)
                       .Select(c => new SelectListItem()
                       {
                           Value = c.Id.ToString(),
                           Text = c.Title
                       }).ToList();
                });

            return subCategories;
        }
    }
}
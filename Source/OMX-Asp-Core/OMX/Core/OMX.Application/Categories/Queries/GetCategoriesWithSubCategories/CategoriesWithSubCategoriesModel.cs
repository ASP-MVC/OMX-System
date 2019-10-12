namespace OMX.Application.Categories.Queries.GetCategoriesWithSubCategories
{
    using OMX.Domain;
    using System;
    using System.Linq.Expressions;

    public class CategoriesWithSubCategoriesModel
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public static Expression<Func<Category, CategoriesWithSubCategoriesModel>> Projection
            => p => new CategoriesWithSubCategoriesModel
            {
                Id = p.Id,
                Title = p.Title,
            };
    }
}

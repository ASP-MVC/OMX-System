namespace OMX.Application.Categories.Queries.GetCategorySubCategories
{
    using OMX.Domain;
    using System;
    using System.Linq.Expressions;

    public class SimplifiedSubcategoryModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string CategoryName { get; set; }

        public static Expression<Func<SubCategory, SimplifiedSubcategoryModel>> Projection
           => p => new SimplifiedSubcategoryModel
           {
               Id = p.Id,
               Title = p.Title,
               CategoryName = p.Category.Title
           };
    }
}

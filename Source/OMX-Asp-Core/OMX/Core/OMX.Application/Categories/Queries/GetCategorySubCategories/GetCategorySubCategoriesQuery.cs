namespace OMX.Application.Categories.Queries.GetCategorySubCategories
{
    using MediatR;
    using System.Collections.Generic;

    public class GetCategorySubCategoriesQuery : IRequest<IEnumerable<SimplifiedSubcategoryModel>>
    {
        public int CategoryId { get; set; }
    }
}

namespace OMX.Application.Categories.Queries.GetCategoriesWithSubCategories
{
    using MediatR;
    using System.Collections.Generic;

    public class CategoriesWithSubCategoriesQuery : IRequest<IEnumerable<CategoriesWithSubCategoriesModel>>
    {
        public int? TakeCount { get; set; }
    }
}

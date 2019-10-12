namespace OMX.Application.Categories.Queries.GetCategoriesWithSubCategories
{
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CategoriesWithSubCategoriesHandler : IRequestHandler<CategoriesWithSubCategoriesQuery, CategoriesWithSubCategoriesModel>
    {
        public Task<CategoriesWithSubCategoriesModel> Handle(CategoriesWithSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

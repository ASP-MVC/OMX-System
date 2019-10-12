namespace OMX.Application.Categories.Queries.GetCategorySubCategories
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.MVC.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class GetCategorySubCategoriesHandler : IRequestHandler<GetCategorySubCategoriesQuery, IEnumerable<SimplifiedSubcategoryModel>>
    {
        private readonly OMXDbContext _context;

        public GetCategorySubCategoriesHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SimplifiedSubcategoryModel>> Handle(GetCategorySubCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.SubCategories
                .Where(x => x.CategoryId == request.CategoryId)
                .Select(SimplifiedSubcategoryModel.Projection)
                .ToListAsync();
        }
    }
}

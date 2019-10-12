namespace OMX.Application.Categories.Queries.GetCategoriesWithSubCategories
{
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using OMX.MVC.Persistence;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class CategoriesWithSubCategoriesHandler : IRequestHandler<CategoriesWithSubCategoriesQuery, IEnumerable<CategoriesWithSubCategoriesModel>>
    {
        private readonly OMXDbContext _context;

        public CategoriesWithSubCategoriesHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriesWithSubCategoriesModel>> Handle(CategoriesWithSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Categories
                      .Where(c => c.SubCategories.Any())
                      .OrderByDescending(x => x.Visit);

            if (request.TakeCount.HasValue)
            {
                return await query
                    .Take(request.TakeCount.GetValueOrDefault())
                    .Select(CategoriesWithSubCategoriesModel.Projection)
                    .ToListAsync();
            }
           
            return await query
                .Select(CategoriesWithSubCategoriesModel.Projection)
               .ToListAsync();
        }
    }
}

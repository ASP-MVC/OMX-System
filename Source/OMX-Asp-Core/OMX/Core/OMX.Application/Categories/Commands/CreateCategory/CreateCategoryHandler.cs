namespace OMX.Application.Categories.Commands.CreateCategory
{
    using MediatR;
    using OMX.Domain;
    using OMX.MVC.Persistence;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Unit>
    {
        private OMXDbContext _context;

        public CreateCategoryHandler(OMXDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Title = request.Title,
            };

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}

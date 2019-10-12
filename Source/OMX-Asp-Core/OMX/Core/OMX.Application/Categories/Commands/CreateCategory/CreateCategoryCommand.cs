namespace OMX.Application.Categories.Commands.CreateCategory
{
    using MediatR;

    public class CreateCategoryCommand : IRequest
    {
        public string Title { get; set; }
    }
}

namespace OMX.Application.Ads.Commands
{
    using MediatR;

    public class DeleteAdCommand : IRequest<string>
    {
        public int AdId { get; set; }
    }
}

using MediatR;

namespace NetCoreApiBoilerplate.Mediators.Samples
{
    /// <summary>
    /// Example MediatR Requests
    /// </summary>
    public class ExampleRequest : IRequest<ExampleRequestResult>
    {
    }

    public class ExampleRequestHandler : IRequestHandler<ExampleRequest, ExampleRequestResult>
    {
#pragma warning disable CS1998 
        public async Task<ExampleRequestResult> Handle(ExampleRequest request, CancellationToken cancellationToken)
        {
            return new ExampleRequestResult
            {
                Id = 1,
                Data = "Hello From Mediatr"
            };
        }
    }
#pragma warning restore CS1998

    public class ExampleRequestResult
    {
        public int Id { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}

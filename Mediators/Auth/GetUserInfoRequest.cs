using MediatR;
using NetCoreApiBoilerplate.Areas;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;

namespace NetCoreApiBoilerplate.Mediators.Auth
{
    public class GetUserInfoRequest : IRequest<UserInfoResource>
    {
        public string userGuid { get; set; }
    }

    public class GetUserInfoHandler : IRequestHandler<GetUserInfoRequest, UserInfoResource>
    {
        private readonly UnitOfWork unitOfWork;

        public GetUserInfoHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<UserInfoResource> Handle(GetUserInfoRequest request, CancellationToken cancellationToken)
        {
            var resource = await unitOfWork.UserService.GetUserInfoAsyncByGuid(request.userGuid);
            return resource;
        }
    }
}

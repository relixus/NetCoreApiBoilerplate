using MediatR;
using NetCoreApiBoilerplate.Areas;
using NetCoreApiBoilerplate.BLL.Areas.Common.Models;

namespace NetCoreApiBoilerplate.Mediators.Auth
{
    public class ChangePasswordRequest : IRequest<OperationResultResource>
    {
        public string UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, OperationResultResource>
    {
        private readonly UnitOfWork unitOfWork;

        public ChangePasswordHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResultResource> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.UserService.ChangePassword(request.UserId, request.OldPassword,request.NewPassword);
        }
    }
}

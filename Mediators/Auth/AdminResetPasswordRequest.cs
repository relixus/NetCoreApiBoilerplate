using MediatR;
using NetCoreApiBoilerplate.Areas;
using NetCoreApiBoilerplate.BLL.Areas.Common.Models;

namespace NetCoreApiBoilerplate.Mediators.Auth
{
    public class AdminResetPasswordRequest : IRequest<OperationResultResource>
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }

    public class AdminResetPasswordHandler : IRequestHandler<AdminResetPasswordRequest, OperationResultResource>
    {
        private readonly UnitOfWork unitOfWork;

        public AdminResetPasswordHandler(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResultResource> Handle(AdminResetPasswordRequest request, CancellationToken cancellationToken)
        {
            return await unitOfWork.UserService.AdminResetPassword(request.UserId, request.NewPassword);
        }
    }
}

using MediatR;
using NetCoreApiBoilerplate.Context;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.BLL.Areas.Common.Services;

namespace NetCoreApiBoilerplate.Mediators.Auth
{
    public class CreateClaimTemplateRequest : IRequest<UserClaimsTemplate>
    {
        public string Area { get; set; }
        public string ClaimName { get; set; }
        public string Description { get; set; }
    }

    public class CreateClaimTemplateHandler : IRequestHandler<CreateClaimTemplateRequest, UserClaimsTemplate>
    {
        private readonly UserContext userContext;

        public CreateClaimTemplateHandler(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<UserClaimsTemplate> Handle(CreateClaimTemplateRequest request, CancellationToken cancellationToken)
        {
            var repository = new CommonRepository<UserClaimsTemplate>(userContext);

            repository.Insert(new UserClaimsTemplate()
            {
                Area = request.Area,
                ClaimName = request.ClaimName,
                Description = request.Description,
                Active = true
            });

            var id = await userContext.SaveChangesAsync();

            var result = repository.GetByID(id);

            return result!;
        }
    }


}

using MediatR;
using Microsoft.AspNetCore.Identity;
using NetCoreApiBoilerplate.BLL.Areas.Auth.Models;
using NetCoreApiBoilerplate.Context;
using System.Security.Claims;
using NetCoreApiBoilerplate.BLL.Areas.Common.Services;

namespace NetCoreApiBoilerplate.Mediators.Auth
{
    public class SetUserClaimsRequest : IRequest
    {
        public string UserGuid { get; set; }
        public int[] ClaimTemplateIds { get; set; }
    }

    public class SetUserClaimsHandler : IRequestHandler<SetUserClaimsRequest>
    {
        private readonly UserContext _userContext;
        private readonly UserManager<ApiUser> _userManager;

        public SetUserClaimsHandler(UserContext userContext, UserManager<ApiUser> userManager)
        {
            _userContext = userContext;
            _userManager = userManager;
        }

        public async Task Handle(SetUserClaimsRequest request, CancellationToken cancellationToken)
        {
            var repository = new CommonRepository<UserClaimsTemplate>(_userContext);

            var user = await _userManager.FindByIdAsync(request.UserGuid);

            var claims = await _userManager.GetClaimsAsync(user);

            var selectedTemplates = repository.Get(f => request.ClaimTemplateIds.Contains(f.UserClaimsTemplateId));

            //GET THE VALUES ON SELECTED TEMPLATES NOT IS CLAIMS TABLE
            var toAdd = selectedTemplates.Where(st => !claims.Select(c=>c.Value).Contains(st.ClaimName));

            await _userManager.AddClaimsAsync(user, MapTemplateToClaim(toAdd).ToArray());
            
            //GET THE CURRENT SAVED CLAIMS NOT IN NEW SELECTION
            var toDelete = claims.Where(c => !selectedTemplates.Select(st=>st.ClaimName).Contains(c.Value));

            await _userManager.RemoveClaimsAsync(user, toDelete.ToArray());
        }

        private IEnumerable<Claim> MapTemplateToClaim(IEnumerable<UserClaimsTemplate> toadd)
        {
            return toadd.Select(s => new Claim(s.Area, s.ClaimName)).ToList();
        }

      
    }
}

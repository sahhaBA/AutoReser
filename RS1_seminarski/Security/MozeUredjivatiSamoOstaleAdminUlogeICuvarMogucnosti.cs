using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RS1_seminarski.Security
{
    public class MozeUredjivatiSamoOstaleAdminUlogeICuvarMogucnosti :
        AuthorizationHandler<UpravljanjeAdminUlogomIZahtjevimaMogucnosti>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public MozeUredjivatiSamoOstaleAdminUlogeICuvarMogucnosti(
                IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            UpravljanjeAdminUlogomIZahtjevimaMogucnosti requirement)
        {
            string LogiraniAdminId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

            string AdminKojegUredjujemoId = httpContextAccessor.HttpContext.Request.Query["Id"].ToString();

            if (context.User.IsInRole("Admin") && context.User.HasClaim(claim => claim.Type == "Uređivanje" && claim.Value == "true")
                && AdminKojegUredjujemoId.ToLower() != LogiraniAdminId.ToLower())
            {
                context.Succeed(requirement);
            }

            if (context.User.IsInRole("Admin") && context.User.HasClaim(claim => claim.Type == "Brisanje" && claim.Value == "true")
                && AdminKojegUredjujemoId.ToLower() != LogiraniAdminId.ToLower())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}




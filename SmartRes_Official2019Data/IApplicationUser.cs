using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace SmartRes_Official2019Data
{
    public interface IApplicationUser
    {
        string FullName { get; set; }
        string Gender { get; set; }
        string physicalAddr { get; set; }

        Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager);
    }
}
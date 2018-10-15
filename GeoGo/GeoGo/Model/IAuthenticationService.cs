using System.Threading.Tasks;
using IdentityModel.OidcClient;

namespace GeoGo.Model
{
    public interface IAuthenticationService
    {
        Task<LoginResult> Authenticate();
        Task LogoutRequest();
    }
}
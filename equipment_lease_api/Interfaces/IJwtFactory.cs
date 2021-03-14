
using equipment_lease_api.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace equipment_lease_api.Auth
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}

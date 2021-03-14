
namespace equipment_lease_api.Interfaces
{
    public interface ITokenFactory
    {
        string GenerateToken(int size= 32);
    }
}

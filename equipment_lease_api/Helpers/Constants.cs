
namespace equipment_lease_api.Helpers
{
    public static class Constants
    {
        public static class Strings
        {
            public static class JwtClaimIdentifiers
            {
                public const string Rol = "rol", Id = "id";
            }

            public static class JwtClaims
            {
                public const string ApiAccess = "api_access";
            }

            public static class AssetStatus
            {
                public const string 
                    OPERATIONAL = "Operational", 
                    BROKEN_DOWN = "Broken Down", 
                    SUNK = "Sunk",
                    LEASED_OUT = "Leased Out",
                    RETURNED = "Returned",
                    AVAILABLE = "Available";
            }
        }
    }
}

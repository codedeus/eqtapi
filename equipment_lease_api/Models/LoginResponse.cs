using System.Collections.Generic;

namespace equipment_lease_api.Models
{
    public class LoginResponse 
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }
        public string Role { get; }
        public int DepartmentId { get; }

        public LoginResponse(AccessToken accessToken, string refreshToken, string role, int departmentId) 
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Role = role;
            DepartmentId = departmentId;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } // EF migrations require at least private setter - won't work on auto-property
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public string Role { get; set; }
        public Department Department { get; set; }
        public ICollection<AssetItem> AssetItems { get; set; }
        public ICollection<AssetLeaseEntry> AssetLeaseEntries { get; set; }
    }
}

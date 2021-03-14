using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace equipment_lease_api.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetUserId()
        {
            var user = this.User.Claims.First(i => i.Type == "id");
            string userId = "";
            if (user != null)
            {
                userId = user.Value;
            }
            return userId;
        }
    }
}

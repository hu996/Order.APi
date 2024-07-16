using Microsoft.AspNetCore.Identity;
using Order.Core.Entities;

namespace Order.Repository.Entities
{
    public class AppUser:IdentityUser
    {
        public string DisplayName { get; set; }
    }
}

using Order.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.TokenServices
{
    public interface ITokenServices
    {
        string CreateToken(AppUser appUser);
    }
}

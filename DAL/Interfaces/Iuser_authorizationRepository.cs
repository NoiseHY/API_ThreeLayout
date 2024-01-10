using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public partial interface Iuser_authorizationRepository
    {
        List<user_authorization> Getuser_authorization();
    }
}

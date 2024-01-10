using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public partial interface Iuser_authorizationBusiness
    {
        List<user_authorization> Getuser_authorization();
    }
}

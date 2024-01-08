using BLL.Inerfaces;
using DAL.Interfaces;
using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bussiness
{
    public partial class categoryBusiness : IcategoryBusiness
    {
        private IcategoryRepository _icategory;
        public categoryBusiness(IcategoryRepository icategory) 
        {
            _icategory = icategory;
        }
        public List<category> GetCategories()
        {
            return _icategory.GetCategories();
        }

        public List<category> GetNameCategories(int id)
        {
            return _icategory.GetNameCategories(id);
        }
    }
}

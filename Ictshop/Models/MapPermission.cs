using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ictshop.Models
{
    public class MapPermission
    {
        ShopManagement db = new ShopManagement();

        public bool checkPermisson(int RoleId,string FunctionCode)
        {

            if(db.Permissions.Count(m => m.RoleId == RoleId && m.FunctionCode == FunctionCode) > 0)
            {
                return true;
            }
            return false;   
        }
    }
}
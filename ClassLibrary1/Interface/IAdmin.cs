using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interface
{
    public enum Access_level { admin, user }
    interface IAdmin
    {
        int Admin_id { get; set; }
        string Admin_name { get; set; }
        string Admin_password { get; set; }
    }
}

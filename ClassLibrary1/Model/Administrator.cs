using ClassLibrary1.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model
{
    public class Administrator : IAdmin
    {
        public int Admin_id { get; set; }
        public string Admin_name { get; set; }
        public string Admin_password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Interface
{
    public enum Access_level { admin, reader }
    interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
        string Login { get; set; }
        string Password { get; set; }
    }
}

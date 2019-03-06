using ClassLibrary1.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    interface IReader
    {
        int Reader_id { get; set; }
        string Name { get; set; }  
        string Login { get; set; }
        string Password { get; set; }
        bool IsBlock { get; set; }
        string Address { get; set; }
        string Contact { get; set; }
        string Email { get; set; }
        string Issue_tag { get; set; }
        string Tags_used { get; set; }
    }
}

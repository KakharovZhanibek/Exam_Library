using ClassLibrary1.Interface;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LIB.Model
{
    public class Reader : IReader
    {
        public int Reader_id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsBlock { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Issue_tag { get; set; }
        public string Tags_used { get; set; }
    }
}
